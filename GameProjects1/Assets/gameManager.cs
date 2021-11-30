using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

using UnityEngine.Audio;

public class gameManager : MonoBehaviour
{
    public List<GameObject> gameObjects;
    public List<AudioSource> soundGroup;

    public static bool isPaused = false;
    public int missingparts = 0;

    public AudioMixer auMix;

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

        foreach(AudioSource audio in soundGroup)
        {
            audio.Pause();
        }

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

        foreach (AudioSource audio in soundGroup)
        {
            audio.UnPause();
        }

        pausePanel.SetActive(false);
        settingPanel.SetActive(false);
        crosshairPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.Locked;
        isPaused = false;
    }

    public void Exit()
    {
        Debug.Log("Exit-ed a Game");
    }

    public void Restart()
    {
        Debug.Log("Restart-ed a Game");
    }

    public void changeVolume(float volume)
    {
        auMix.SetFloat("Volume", volume);
    }
}
