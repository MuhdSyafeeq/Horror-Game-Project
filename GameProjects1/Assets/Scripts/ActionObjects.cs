using UnityEngine;
using System.Collections.Generic;

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
    [Space(5)]

    public gameManager gm;

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
            gm.missingparts += 1;
            gm.UpdateText();

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

                        currMsg = "Press E to Turn Off Fan";
                        //Debug.Log("Stop Fan");
                    }
                    else
                    {
                        fan.Play("Fan");
                        this.Invoke(() => fan.Play("Fan 2"), 1f);
                        this.Invoke(() => fan.Stop("Fan 3"), 2f);

                        currMsg = "Press E to Turn On Fan";
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
            fl.transform.localPosition = Vector3.zero;
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

            /*
            SphereCast cameraV;
            if (iObj.fpsCam.gameObject.TryGetComponent<SphereCast>(out cameraV))
            {
                fl.transform.LookAt(iObj.fpsCam.gameObject.transform.position + (cameraV.origin + cameraV.direction * cameraV.maxDistance));
            }
            */
        }

        else if(iObj.theHitObj.name == "Papers")
        {
            currMsg = "Press E to look at Papers";
            if (isHoldingPaper == false)
            {
                GameObject fl = iObj.theHitObj.gameObject;
                fl.transform.SetParent(iObj.viewObject.transform);
                fl.transform.localPosition = Vector3.zero;
                fl.transform.localRotation = Quaternion.identity;
                fl.transform.localEulerAngles = new Vector3(0, 180, 0);
                fl.transform.localScale *= 2f;
                isHoldingPaper = true;
            }
            else
            {
                isHoldingPaper = false;
                GameObject fl = iObj.theHitObj.gameObject;
                //GameObject Pages = iObj.viewObject.gameObject;
                //Debug.Log($"Analysing Objects Named -> {Pages.name}");
                iObj.theHitObj = null;
                iObj.actObj = null;
                /**
                    foreach (GameObject chilObject in Pages.gameObject.transform)
                    {
                        if(chilObject.name == "Papers")
                        {
                            Destroy(chilObject.gameObject, 2.5f);
                        }
                    }
                **/
                Destroy(fl.gameObject);
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
