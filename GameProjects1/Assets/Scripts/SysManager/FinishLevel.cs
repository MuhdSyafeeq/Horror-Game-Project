using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLevel : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("FINSIHED");
        gameManager.Instance.Finish();
    }
}
