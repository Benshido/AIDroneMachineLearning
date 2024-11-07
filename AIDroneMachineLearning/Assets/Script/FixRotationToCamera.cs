using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixRotationToCamera : MonoBehaviour
{
    void Update()
    {
        // Get the main camera
        Camera cam = Camera.main;

        // Check if the camera is available
        if (cam != null)
        {
            // Rotate the UI element to face the camera
            Vector3 directionToCamera = cam.transform.position - transform.position;
            directionToCamera.y = 0; // Keep the UI facing horizontally
            Quaternion targetRotation = Quaternion.LookRotation(directionToCamera);
            transform.rotation = targetRotation;
        }

    }
}
