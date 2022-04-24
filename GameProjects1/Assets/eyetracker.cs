using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eyetracker : MonoBehaviour
{
    public MouseLook getData;

    // Update is called once per frame
    void FixedUpdate(){
        float mouseX = Input.GetAxis("Mouse X") * getData.mouseSensitiviy * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * getData.mouseSensitiviy * Time.deltaTime;
        
        transform.LookAt(new Vector3(mouseX, mouseY));

        // Look Left or Right (Side-to-Side)
        //playerBody.Rotate(Vector3.up * mouseX);

        // Look Up or Down (No-Clamping Method)
        //xRotation -= mouseY;
        //xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        //transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
