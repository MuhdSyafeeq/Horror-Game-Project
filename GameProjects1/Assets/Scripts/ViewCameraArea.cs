using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewCameraArea : MonoBehaviour
{
    [SerializeField] private GameObject MainObject;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 12) { MainObject.layer = 10; }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 12) { MainObject.layer = 14; }
    }
}
