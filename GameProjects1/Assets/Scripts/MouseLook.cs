using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitiviy = 100f;
    public Slider mouseSlider;
    public TMP_InputField mouseInput;

    public Transform playerBody;

    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void changeSensitivity(string mouseSens)
    {
        float newData;
        if(float.TryParse(mouseSens, out newData))
        {
            changeSensitivity(newData);
        }
    }

    public void changeSensitivity(float mouseSens)
    {
        if (mouseSens == 0) { mouseSens = 1; }
        else if(mouseSens > 10) { mouseSens = 10; }
        else
        {
            mouseSensitiviy = (mouseSens / (100f / 10f)) * 100f;
            mouseSlider.value = mouseSensitiviy / 10f;
            mouseInput.text = System.Math.Round(mouseSlider.value, 2).ToString();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.isPaused != true)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitiviy * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitiviy * Time.deltaTime;

            // Look Left or Right (Side-to-Side)
            playerBody.Rotate(Vector3.up * mouseX);

            // Look Up or Down (Clamping Method)
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }
    }
}
