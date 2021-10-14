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
        if(slider.value > 0)
        {
            slider.value -= sanity;
        }
    }

    public void regainSanity(float sanity)
    {
        if(slider.value < slider.maxValue)
        {
            slider.value += sanity;
        }
    }

    /*
    private void Update()
    {
        if(slider.value == 0)
        {
            Debug.Log("Out of Sanity");
        }
        if (slider.value == slider.maxValue)
        {
            Debug.Log("Max Sanity");
        }
    }
    */
}
