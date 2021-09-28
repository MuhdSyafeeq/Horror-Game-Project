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
    public bool InAreaLight = false;

    public SphereCast visionRange;

    private void Awake()
    {
        originalH = playerCol.height;
        playerCol.enabled = true;
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
                if(flashlight.enabled == true) {
                    flashlight.enabled = false;
                    visionRange.maxDistance = 2.7f;
                }
                else {
                    flashlight.enabled = true;
                    visionRange.maxDistance = 1.1f;
                }
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
