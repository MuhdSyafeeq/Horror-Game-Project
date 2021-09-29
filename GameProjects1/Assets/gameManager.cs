using UnityEngine;

public class gameManager : MonoBehaviour
{
    public GameObject menu;
    public static bool isPaused = false;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        menu.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        isPaused = true;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        menu.SetActive(false);
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
}
