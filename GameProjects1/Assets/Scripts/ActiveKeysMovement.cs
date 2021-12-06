using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveKeysMovement : MonoBehaviour
{
    public float speedModifier = 1.2f;
    public GameObject playerHand;
    public gameManager gm;

    public CharacterController playerCol;
    public float originalH;
    public float reducedH;

    public SphereCast visionRange;
    public Animator anim;

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
                    visionRange.maxDistance = 1.1f;
                }
                else {
                    flashlight.enabled = true;
                    visionRange.maxDistance = 3.5f;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            gm.Pause();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            GetComponent<PlayerMovement>().speed *= speedModifier;
            anim.SetTrigger("inputShift");
        }
        if (Input.GetKeyUp(KeyCode.LeftShift)) {
            GetComponent<PlayerMovement>().speed /= speedModifier;
            anim.SetTrigger("inputShift");
        }

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
