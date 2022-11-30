using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightArea : MonoBehaviour
{
    bool isNearPlayer = false;
    public float myRadius;
    public LayerMask layerMask;

    private float calmDuration = 3f;
    private float TimeAt_Light = 0f;

    public SphereCast playerViewSphere;
    public Insanity playerSanity;
    public Light powerLight;

    private void Awake()
    {
        if(powerLight == null)
        {
            powerLight = this.gameObject.GetComponent<Light>();
        }
        if(myRadius == 0)
        {
            myRadius = powerLight.range;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.Instance.isPaused != true)
        {
            if (powerLight.enabled == true)
            {
                isNearPlayer = Physics.CheckSphere(this.transform.position, myRadius, layerMask);
                if (isNearPlayer == true)
                {
                    //Debug.Log("Hit An Entity");
                    if (playerSanity.slider.value <= playerSanity.slider.maxValue)
                    {
                        playerSanity.regainSanity(((calmDuration * .75f + TimeAt_Light) * playerViewSphere.fearMult) * Time.deltaTime);
                        
                        if (calmDuration > 0)
                        {
                            calmDuration -= 1.5f; //.05f;
                        }
                        if(TimeAt_Light < 1f)
                        {
                            TimeAt_Light += .01f;
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
