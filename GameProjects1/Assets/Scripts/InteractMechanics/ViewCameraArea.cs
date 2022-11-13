using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewCameraArea : MonoBehaviour
{
    [SerializeField] private GameObject MainObject;
    [SerializeField] private GameObject CameraObj;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 12)
        {
            int counter = 0;
            foreach(GameObject checkObj in MainObject.GetComponent<ActionObjects>().connectedObj)
            {
                if(checkObj.name == CameraObj.name) { break; }
                counter = counter + 1;
            }
            
            if(counter == MainObject.GetComponent<ActionObjects>().connectedObj.Count)
            {
                MainObject.GetComponent<ActionObjects>().connectedObj.Add(CameraObj);
            }

            counter = 0;
            MainObject.layer = 10;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 12)
        {
            MainObject.GetComponent<ActionObjects>().connectedObj.Remove(CameraObj);

            MainObject.layer = 14;
        }
    }
}
