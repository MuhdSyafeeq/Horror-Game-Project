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
    #region Singular-Instance
    public static gameManager Instance { get; private set; }

    //If a script will be using the singleton in its awake method, make sure the manager is first to execute with the Script Execution Order project settings
    void Awake()
    {
        if (Instance != null) //this depend how you want to handle multiple managers (like when switching/adding scenes) but this way should cover common use cases
            Destroy(Instance);
        Instance = this;

        SoundSystem = GameObject.FindSceneObjectsOfType(typeof(AudioSource)) as AudioSource[];

        foreach(AudioSource aSystem in SoundSystem)
        {
            soundGroup.Add(aSystem);
        }
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
    #endregion

    public List<GameObject> gameObjects;
    public List<AudioSource> soundGroup;
    AudioSource[] SoundSystem;

    public int missingparts = 0;
    public float dataSens = 0;
    public float dataSoundVol = 0;

    public bool isPaused = false;

    public bool isViewArea = false;
    public bool isResetView = false;

    public GameObject lastHitObj;
    //public Transform lastCamView;
    public AudioMixer auMix;

    public TMP_InputField volumeInput;
    public Slider volumeSlider;

    public void UpdateText()
    {
        GameObject textPanel = gameObjects.Where(obj => obj.name == "Text-Updater").SingleOrDefault();
        textPanel.GetComponent<Text>().text = "Missing Parts " + missingparts.ToString() + " / 6";
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

    public void Finish()
    {
        GameObject finishPanel = gameObjects.Where(obj => obj.name == "Ui-Finish-Panel").SingleOrDefault();
        GameObject crosshairPanel = gameObjects.Where(obj => obj.name == "Crosshair").SingleOrDefault();

        finishPanel.SetActive(true);
        crosshairPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Confined;
        isPaused = true;

        /*
        foreach(AudioSource audio in soundGroup)
        {
            audio.Pause();
        }
        */
    }

    public void GameOver()
    {
        GameObject gameOver = gameObjects.Where(obj => obj.name == "Ui-GameOver-Panel").SingleOrDefault();
        GameObject crosshairPanel = gameObjects.Where(obj => obj.name == "Crosshair").SingleOrDefault();

        gameOver.SetActive(true);
        crosshairPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Confined;
        isPaused = true;

        /*
        foreach(AudioSource audio in soundGroup)
        {
            audio.Pause();
        }
        */
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
        Application.Quit();
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Confined;
        isPaused = false;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //foreach (AudioSource audio in soundGroup) { audio.UnPause(); }
        SaveSystem.LoadData(this);
        Debug.Log($"[AFTER RESET] Game System is Paused? -> { isPaused }");
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Confined;
        isPaused = false;


        
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //foreach (AudioSource audio in soundGroup) { audio.UnPause(); }
        //Debug.Log($"[Starting...] Game System is Paused? -> { isPaused }");
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
        dataSoundVol = calc;
        SaveSystem.SaveData(this);
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
