using UnityEngine;
using UnityEngine.UI;

public class Insanity : MonoBehaviour
{
    public Slider slider;
    public float valIndifference;

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
    
    private void Update()
    {
        if(gameManager.Instance.isPaused == false && slider.value == 0)
        {
            Debug.Log($"<color=yellow>Activity Logs -></color> Player's Sanity is <color=blue>{slider.value}</color>");

            Debug.Log("<color=red>Game Over:</color> You Went Insane...");
            gameManager.Instance.isPaused = true;
            this.Invoke(() => gameManager.Instance.GameOver(), 1.5f);
        }

        //if Less than 90%, Start Reduce Pitch
        //if(slider.value <= (0.9f * slider.maxValue))
        //{
        //    float tempValue = slider.value;
        //}
    }
}
