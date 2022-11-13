using UnityEngine;

public class sensor : MonoBehaviour
{
    public Light curLights;

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer == 12 || other.gameObject.layer == 11)
        {
            curLights.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 12 || other.gameObject.layer == 11)
        {
            curLights.enabled = false;
        }
    }
}
