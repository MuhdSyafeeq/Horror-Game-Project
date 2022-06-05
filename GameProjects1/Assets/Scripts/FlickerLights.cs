using UnityEngine;

public class FlickerLights : MonoBehaviour
{
    [SerializeField] private Light light;
    [SerializeField] private bool  FlickerSlower = false;

    float waitTime = 0.0f;

    // Update is called once per frame
    void FixedUpdate(){
        /***
             * Creating Safe Blinks (Avoid Seizure)
             * 
             ***/
        if (FlickerSlower == true)
        {
            if (Mathf.RoundToInt(waitTime) > .75f)
            {
                waitTime = 0.0f; // --> Reset Timer
                if (light.enabled == true) { light.enabled = false; }
                else if(light.enabled == false) { light.enabled = true; }
            }
        }
        else
        {
            if (Mathf.RoundToInt(waitTime) > .25f)
            {
                waitTime = 0.0f; // --> Reset Timer
                if (light.enabled == true) { light.enabled = false; }
                else if(light.enabled == false) { light.enabled = true; }
            }
        }
        waitTime += Time.deltaTime;
    }
}
