using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveKeysMovement : MonoBehaviour
{
    public float speedModifier = 1.2f;

    // Update is called once per frame
    void Update()
    {
        // Flash Sensory (Reduce All Audio to 0.5) (Create Emmitter of Blink when any entity moves)
        if(Input.GetKeyDown(KeyCode.F)) { Debug.Log("Active/Diactive Flash Sensory!"); }
        if (Input.GetKeyDown(KeyCode.Q)) { Debug.Log("Active/Diactive Hearing Ultra-Sensor!"); }
        if (Input.GetKeyDown(KeyCode.LeftShift)) { GetComponent<PlayerMovement>().speed *= speedModifier; }
        if (Input.GetKeyUp(KeyCode.LeftShift)) { GetComponent<PlayerMovement>().speed /= speedModifier; }
    }
}
