using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using TMPro;
using System.Text.RegularExpressions;


public class gameManager : MonoBehaviour
{
    public List<GameObject> gameObjects;
    public List<AudioSource> soundGroup;

    public static bool isPaused = false;
    public int missingparts = 0;

    public AudioMixer auMix;

    public TMP_InputField volumeInput;
    public Slider volumeSlider;

    public void UpdateText()
    {
        GameObject textPanel = gameObjects.Where(obj => obj.name == "Text-Updater").SingleOrDefault();
        textPanel.GetComponent<Text>().text = "Missing Parts " + missingparts.ToString() + "/5";
    }

    public void Pause()
    {
        Time.timeScale = 0f;

        GameObject pausePanel = gameObjects.Where(obj => obj.name == "Ui-Pause-Panel").SingleOrDefault();
        GameObject crosshairPanel = gameObjects.Where(obj => obj.name == "Crosshair").SingleOrDefault();
        /*
        foreach(AudioSource audio in soundGroup)
        {
            audio.Pause();
        }
        */
        pausePanel.SetActive(true);
        crosshairPanel.SetActive(false);

        Cursor.lockState = CursorLockMode.Confined;
        isPaused = true;
    }

    public void Resume()
    {
        Time.timeScale = 1f;

        GameObject pausePanel = gameObjects.Where(obj => obj.name == "Ui-Pause-Panel").SingleOrDefault();
        GameObject crosshairPanel = gameObjects.Where(obj => obj.name == "Crosshair").SingleOrDefault();
        GameObject settingPanel = gameObjects.Where(obj => obj.name == "Setting-Panel").SingleOrDefault();
        /*
        foreach (AudioSource audio in soundGroup)
        {
            audio.UnPause();
        }
        */
        pausePanel.SetActive(false);
        settingPanel.SetActive(false);
        crosshairPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.Locked;
        isPaused = false;
    }

    public void Exit()
    {
        Debug.Log("Exit-ed a Game");
        Application.Quit();
    }

    public void Restart()
    {
        Resume();
        //Debug.Log("Restart-ed a Game");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

    public void changeVolume(float volume)
    {
        float calc;
        if (volume <= 0.0001f)
        {
            calc = -80;
            volumeInput.text = System.Math.Round(0f).ToString() + "%";
        }
        else
        {
            calc = Mathf.Log10(volume) * 20;
            volumeInput.text = System.Math.Round(((float)(volume / 1) * 100), 3).ToString() + "%";
        }
        auMix.SetFloat("Volume", calc);
    }

    public void changeVolume(string volume)
    {
        if (volume.Contains("%"))
        {
            Match m = Regex.Match(volume, @"\d+");
            string RegexToFloat = (string)(m.Value);

            //Debug.Log(RegexToFloat);

            float newData;
            if (float.TryParse(RegexToFloat, out newData))
            {
                volumeInput.text = newData + "%";

                float calculation = (newData / 100) * 1.0f;
                if(calculation == 0) { calculation = 0.0001f; }

                volumeSlider.value = calculation;
                changeVolume(calculation);
            }
            
        }
        else
        {
            float newData;
            if (float.TryParse(volume, out newData))
            {
                volumeInput.text = newData + "%";

                float calculation = (newData / 100) * 1.0f;
                if (calculation == 0) { calculation = 0.0001f; }

                volumeSlider.value = calculation;
                changeVolume(calculation);
            }
        }
    }
}
