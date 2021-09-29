using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightArea : MonoBehaviour
{
    bool isNearPlayer = false;
    public float myRadius = 2f;
    public LayerMask layerMask;

    private float calmDuration = 3f;
    private float fearMult = .3f;
    private float TimeAt_Light = 0f;

    public Insanity playerSanity;
    public Light powerLight;

    private void Awake()
    {
        if(powerLight == null)
        {
            powerLight = this.gameObject.GetComponent<Light>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.isPaused != true)
        {
            if (powerLight.enabled == true)
            {
                isNearPlayer = Physics.CheckSphere(this.transform.position, myRadius, layerMask);
                if (isNearPlayer == true)
                {
                    //Debug.Log("Hit An Entity");
                    if (playerSanity.slider.value <= playerSanity.slider.maxValue)
                    {
                        playerSanity.regainSanity(((calmDuration * 1.1f + TimeAt_Light) * fearMult) * Time.deltaTime);
                        TimeAt_Light += .03f;
                        if (calmDuration > 0)
                        {
                            calmDuration -= .05f;
                        }
                    }
                }
                else
                {
                    TimeAt_Light = 0f;
                    calmDuration = 3f;
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, myRadius);
    }
}
