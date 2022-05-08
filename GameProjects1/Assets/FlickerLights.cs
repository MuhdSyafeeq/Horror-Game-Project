using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerLights : MonoBehaviour
{
    [SerializeField] private Light light;
    float minFlickerSpeed = 1.0f;
    float maxFlickerSpeed = 10.0f;

    // Update is called once per frame
    void FixedUpdate(){
        this.Invoke(() => light.enabled = true, (Random.Range(minFlickerSpeed, maxFlickerSpeed)));
        this.Invoke(() => light.enabled = false, (Random.Range(minFlickerSpeed, maxFlickerSpeed)));
    }
}
