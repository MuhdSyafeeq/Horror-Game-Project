using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveKeysMovement : MonoBehaviour
{
    public float speedModifier = 1.2f;
    public GameObject playerHand;

    public CharacterController playerCol;
    public float originalH;
    public float reducedH;

    private double FearImagination = 0.0;
    private bool InAreaLight = true;

    private void Awake()
    {
        originalH = playerCol.height;
    }

    // Update is called once per frame
    void Update()
    {
        Light flashlight;
        flashlight = playerHand.GetComponentInChildren<Light>();
        if(flashlight != null)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if(flashlight.enabled == true) { flashlight.enabled = false; }
                else { flashlight.enabled = true; }
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftShift)) { GetComponent<PlayerMovement>().speed *= speedModifier; }
        if (Input.GetKeyUp(KeyCode.LeftShift)) { GetComponent<PlayerMovement>().speed /= speedModifier; }

        if (Input.GetKeyDown(KeyCode.LeftControl)) { Crouching(); }
        if (Input.GetKeyUp(KeyCode.LeftControl)) { Standing(); }
    }

    void Crouching()
    {
        playerCol.height = reducedH;
    }

    void Standing()
    {
        playerCol.height = originalH;
    }
}
