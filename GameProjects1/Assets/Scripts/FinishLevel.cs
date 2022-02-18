using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLevel : MonoBehaviour
{
    public gameManager gm;
    public GameObject[] aura;

    private void Update()
    {
        if (gameManager.missingparts == 6)
        {
            foreach (GameObject auras in aura)
            {
                auras.SetActive(true);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("FINSIHED");
        gm.Finish();
    }
}
