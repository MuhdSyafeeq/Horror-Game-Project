using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class ActionObjects : MonoBehaviour
{
    [Header("Object Referenced & List")]
    //public GameObject currentObject;
    public GameObject refObjects;
    public List<GameObject> connectedObj = new List<GameObject>();
    [Space(5)]

    [Header("Current Linked Object")]
    public GameObject linkedInteraction;
    public InteractionObjects iObj;
    [Space(5)]

    [Header("Paper Interaction")]
    public string currMsg = "Press E to Interact";
    public bool isHoldingPaper = false;
    //[Space(5)]

    //public gameManager gm;

    public void InteractUse()
    {
        if(iObj.theHitObj.name == "LightSwitch" || iObj.theHitObj.name == "Lamps")
        {
            if (refObjects != null)
            {
                if (connectedObj.Count > 0)
                {
                    foreach(GameObject obj in connectedObj)
                    {
                        Light light = obj.GetComponent<Light>();
                        if (light.enabled == true) { obj.GetComponent<Light>().enabled = false; }
                        else { obj.GetComponent<Light>().enabled = true; }
                    }
                }

                Light curLight = refObjects.GetComponent<Light>();
                if(curLight.enabled == true)
                {
                    refObjects.GetComponent<Light>().enabled = false;
                    currMsg = "Press E to turn on the lights";
                }
                else
                {
                    refObjects.GetComponent<Light>().enabled = true;
                    currMsg = "Press E to turn off the lights";
                }
            }
            else
            {
                Debug.Log("Unable To Switch The Power, No Object Referenced");
            }
        }

        else if(iObj.theHitObj.name == "mannequin_body" || iObj.theHitObj.name == "mannequin_leg_right"
            || iObj.theHitObj.name == "mannequin_leg_left" || iObj.theHitObj.name == "mannequin_arm_left_b"
            || iObj.theHitObj.name == "mannequin_arm_right_b" || iObj.theHitObj.name == "mannequin_head")
        {
            gameManager.Instance.missingparts += 1;
            gameManager.Instance.UpdateText();

            GameObject fl = iObj.theHitObj.gameObject;
            iObj.theHitObj = null;
            iObj.actObj = null;
            Destroy(fl);
        }

        else if (iObj.theHitObj.name == "ACSwitch") {
            Debug.Log("Aircond Messages");
        }

        else if (iObj.theHitObj.name == "FanSwitch") {
            
            if (refObjects != null)
            {
                Animation fan;
                if(refObjects.TryGetComponent<Animation>(out fan))
                {
                    fan = refObjects.GetComponent<Animation>();
                    if(fan.isPlaying == true)
                    {
                        fan.Play("Fan 2");
                        this.Invoke(() => fan.Play("Fan"), 1f);
                        this.Invoke(() => fan.Stop(), 2f);

                        currMsg = "Press E to Turn On Fan";
                        //Debug.Log("Stop Fan");
                    }
                    else
                    {
                        fan.Play("Fan");
                        this.Invoke(() => fan.Play("Fan 2"), 1f);
                        this.Invoke(() => fan.Stop("Fan 3"), 2f);

                        currMsg = "Press E to Turn Off Fan";
                        //Debug.Log("Turned On Fan");
                    }
                }
                else
                {
                    Debug.Log("Ref Object Fan are Missing?");
                }
            }
            else
            {
                Debug.Log("Unable To Switch Fan, No Object Referenced");
            }
        }

        else if (iObj.theHitObj.name == "Flashlight")
        {
            GameObject fl = iObj.theHitObj.gameObject;
            fl.transform.SetParent(iObj.hands.transform);
            fl.transform.localPosition = new Vector3(0.426f, -1.5f, -0.65f);
            fl.transform.localRotation = Quaternion.identity;
            Rigidbody flashlightRB;
            if(fl.TryGetComponent<Rigidbody>(out flashlightRB))
            {
                fl.GetComponent<Rigidbody>().useGravity = false;
                fl.GetComponent<Rigidbody>().freezeRotation = true;
                fl.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }
            fl.transform.localEulerAngles = new Vector3(90, -4, 0);
            fl.transform.localScale = new Vector3(2f, 2f, 2f);
        }

        else if(iObj.theHitObj.name == "StoriesLeftBehind")
        {
            currMsg = "Press E to look at Papers";
            if (isHoldingPaper == false)
            {
                /***
                GameObject fl = iObj.theHitObj.gameObject;
                fl.transform.SetParent(iObj.viewObject.transform);
                fl.transform.localPosition = Vector3.zero;
                fl.transform.localRotation = Quaternion.identity;
                fl.transform.localEulerAngles = new Vector3(0, 180, 0);
                fl.transform.localScale *= 2f;
                ***/
                foreach(GameObject obj in connectedObj)
                {
                    GameObject currentObj = obj;
                    bool checkTMP = currentObj.TryGetComponent<TextMeshPro>(out TextMeshPro tmp);
                    if(checkTMP == true)
                    {
                        string copyTexts = "<Waiting For Next Value>";
                        if (currentObj.name == "FromText")
                        {
                            copyTexts = tmp.text;
                        }
                        else if (currentObj.name == "AfterText")
                        {
                            tmp.text = copyTexts;
                        }
                    }
                    else {
                        currentObj.SetActive(true);
                    }

                }

                isHoldingPaper = true;
                currMsg = "Press E to crumple the paper";
                gameManager.Instance.isPaused = true;
            }
            else
            {
                gameManager.Instance.isPaused = false;
                foreach (GameObject obj in connectedObj)
                {
                    GameObject currentObj = obj;
                    if (currentObj.name == "ViewPages"){ currentObj.SetActive(false); }
                }
                isHoldingPaper = false;

                GameObject fl = iObj.theHitObj.gameObject;
                iObj.theHitObj = null;
                iObj.actObj = null;
                Destroy(fl.gameObject);
            }
        }

        else if (iObj.theHitObj.name == "OminousBed")
        {
            if (refObjects != null && connectedObj.Count != 0)
            {
                if(gameManager.Instance.isViewArea == false)
                {
                    gameManager.Instance.isViewArea = true;
                    //gameManager.Instance.lastCamView = refObjects.transform;
                    currMsg = "Press E to leave the Bed";

                    foreach (GameObject @object in connectedObj)
                    {
                        if (@object.name == "ViewFrontBed"  ||
                            @object.name == "ViewLeftBed"   ||
                            @object.name == "ViewRightBed"      )
                        { refObjects.GetComponent<CameraOverride>().CameraOvride = @object.transform; }

                        if (@object.name == "FirstPersonPlayer")
                        {
                            @object.GetComponent<PlayerMovement>().enabled = false;
                            @object.GetComponent<ActiveKeysMovement>().enabled = false;
                        }
                        
                        if(@object.name == "Main_Character") { @object.SetActive(false); }
                    }
                }
                else
                {
                    currMsg = "Press E to look under the Bed";
                    refObjects.GetComponent<CameraOverride>().CameraOvride =
                        //gameManager.Instance.lastCamView;
                        refObjects.GetComponent<CameraOverride>().SelfPos.transform;

                    gameManager.Instance.isViewArea = false;
                    gameManager.Instance.isResetView = true;

                    foreach (GameObject @object in connectedObj)
                    {
                        if (@object.name == "FirstPersonPlayer")
                        {
                            @object.GetComponent<PlayerMovement>().enabled          = true;
                            @object.GetComponent<ActiveKeysMovement>().enabled      = true; 
                        }

                        if (@object.name == "Main_Character") { @object.SetActive(true); }
                    }
                    this.Invoke(() => gameManager.Instance.isResetView = false, 5f);
                }
                
            }
            else
            {
                Debug.Log("NULL: No Object Referenced");
            }
        }

        else
        {
            if(refObjects != null)
            {
                refObjects.GetComponent<Animator>().enabled = true;
                string animationName = "isOpen_Obj_";

                if(iObj.theHitObj.name == "tvStandDoor_R") //|| iObj.theHitObj.name == "doorR")
                {
                    animationName = animationName + "2";
                }
                else
                {
                    animationName = animationName + "1";
                }
                /*
                    int count = 0;
                    foreach(AnimatorControllerParameter param_ in refObjects.GetComponent<Animator>().parameters)
                    {
                        //Debug.Log($"Comparing --> {animationName} << >> {param_.name}");
                        if(animationName == param_.name) { break; }
                        count++;
                    }

                    if(count == refObjects.GetComponent<Animator>().parameterCount)
                    {
                        animationName = "isOpen_Obj_1";
                    }
                */
                if( refObjects.GetComponent<Animator>().GetBool(animationName) == false )
                {
                    refObjects.GetComponent<Animator>().SetBool(animationName, true);
                    currMsg = "Press E to Close";
                    if (iObj.theHitObj.name == "doorR" || iObj.theHitObj.name == "doorL")
                    {
                        linkedInteraction.GetComponent<ActionObjects>().currMsg = this.currMsg;
                    }
                }
                else
                {
                    refObjects.GetComponent<Animator>().SetBool(animationName, false);
                    currMsg = "Press E to Open";
                    if (iObj.theHitObj.name == "doorR" || iObj.theHitObj.name == "doorL")
                    {
                        linkedInteraction.GetComponent<ActionObjects>().currMsg = this.currMsg;
                    }
                }
            }
            else
            {
                Debug.Log("Unable to Retrieve Animation of Objects, Do Nothing.");
            }
        }
    }
}
