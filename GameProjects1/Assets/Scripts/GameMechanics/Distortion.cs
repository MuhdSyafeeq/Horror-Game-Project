using UnityEngine;
using UnityEngine.UI;

public class Distortion : MonoBehaviour
{
    [SerializeField] private Insanity Insanity;

    [SerializeField] private bool LowSanity = false;

    // Update is called once per frame
    void LateUpdate()
    {
        if (Insanity.slider.value < (Insanity.slider.maxValue - 70)){ LowSanity = true; }

        if (LowSanity == true)
        {
            foreach(AudioSource AuSource in gameManager.Instance.soundGroup)
            {
                if(AuSource.pitch >= .32f) { AuSource.pitch -= (.5f * Time.deltaTime); }
            }
        }
    }
}
