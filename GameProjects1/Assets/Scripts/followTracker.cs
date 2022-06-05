using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followTracker : MonoBehaviour
{
    [SerializeField] private GameObject mouseTracks;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.LookAt(mouseTracks.transform.position);
    }
}
