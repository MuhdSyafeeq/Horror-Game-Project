using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveKeysMovement : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField]
    public GameObject playerHand;
    public float speedModifier = 1.2f;
    //public gameManager gm;

    [Header("Character Settings")]
    [SerializeField]
    public CharacterController playerCol;
    public float originalH;
    public float reducedH;

    [Header("View Distance Settings")]
    [SerializeField]
    public SphereCast visionRange;
    public bool hasFlashlight = false;
    public Light spotLights;

    [Header("Animations Settings")]
    [SerializeField]
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
        if(flashlight != null && spotLights != null) { hasFlashlight = true; }
        if (hasFlashlight && Input.GetKeyDown(KeyCode.F))
        {
            if (spotLights.enabled == true)
            {
                spotLights.enabled = false;
                visionRange.maxDistance = 5.5f; //1.1f
            }
            else
            {
                spotLights.enabled = true;
                visionRange.maxDistance = 10.5f; //3.5f
            }
        }
        

        if (Input.GetKeyDown(KeyCode.P))
        {
            gameManager.Instance.Pause();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            GetComponent<PlayerMovement>().speed *= speedModifier;
            if(anim.GetFloat("inputH") != 0 && 
                anim.GetFloat("inputV") != 0)
            {
                anim.SetTrigger("inputShift");
            }
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
