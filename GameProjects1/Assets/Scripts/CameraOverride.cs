using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOverride : MonoBehaviour
{
    [SerializeField] private Transform CameraOvride;

    [SerializeField] private float smoothSpeed = .125f;
    [SerializeField] private Vector3 offset;

    // Update is called once per frame
    void FixedUpdate()
    {
        if(gameManager.Instance.isViewArea == true)
        {
            Vector3 desiredPosition = CameraOvride.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

        }
    }
}
