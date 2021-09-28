using UnityEngine;
using UnityEngine.UI;

public class Insanity : MonoBehaviour
{
    public Slider slider;

    public void SetMaxSanity(float sanity)
    {
        slider.maxValue = sanity;
        slider.value = sanity;
    }

    public void setSanity(float sanity)
    {
        slider.value = sanity;
    }

    public void reduceSanity(float sanity)
    {
        slider.value -= sanity;
    }

    public void regainSanity(float sanity)
    {
        slider.value += sanity;
    }
}
