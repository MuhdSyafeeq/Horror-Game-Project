﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitiviy = 100f;

    public Transform playerBody;

    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
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
