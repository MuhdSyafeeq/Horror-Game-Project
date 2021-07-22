using UnityEngine;

public class ActionObjects : MonoBehaviour
{
    public GameObject inRangeObjects;
    public GameObject refObjects;
    public GameObject linkedInteraction;
    public InteractionObjects iObj;

    public string currMsg = "Press E to Interact";

    public void InteractUse()
    {
        if(iObj.theHitObj.name == "SwitchBox")
        {
            if(refObjects != null)
            {
                Light curLight = refObjects.GetComponent<Light>();
                if(curLight.enabled == true)
                {
                    refObjects.GetComponent<Light>().enabled = false;
                    currMsg = "Press E to Turn On Lights";
                }
                else
                {
                    refObjects.GetComponent<Light>().enabled = true;
                    currMsg = "Press E to Turn Off Lights";
                }
            }
            else
            {
                Debug.Log("Unable To Switch Lights, No Object Referenced");
            }
        }

        else if (iObj.theHitObj.name == "Flashlight")
        {
            GameObject fl = iObj.theHitObj.gameObject;
            fl.transform.SetParent(iObj.hands.transform);
            fl.transform.localPosition = Vector3.zero;
            fl.transform.localRotation = Quaternion.identity;
            //fl.transform.localEulerAngles = new Vector3(90, 0, 0);
            //fl.transform.localScale = new Vector3(5, 5, 5);
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
