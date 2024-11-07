using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedDotMarker : MonoBehaviour
{
    public RawImage marker;
    public float fixedSize = 0.0000000000001f;
    private Camera mainCam;

    private float startingDistance;
    private Vector3 startingScale;
    private bool inRangeOfCommander = false;
    private bool markerManuallyDisabled = false;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;

        //Get starting distance to scale objects by, this is the control.
        startingDistance = Vector3.Distance(mainCam.transform.position, transform.position);
        //Get starting scale of the object, in the previous version it would have scaled everything to one.
        startingScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - mainCam.transform.position);
        mainCam.WorldToScreenPoint(transform.position);

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (marker.enabled)
            {
                marker.enabled = false;
                markerManuallyDisabled = true;
            }
            else
            {
                if (!inRangeOfCommander)
                {
                    marker.enabled = true;
                    markerManuallyDisabled = false;
                }
            }
        }
    }

    public void DisableMarker()
    {
        marker.enabled = false;
        inRangeOfCommander = true;
    }

    public void EnableMarker()
    {
        if (!markerManuallyDisabled)
            marker.enabled = true;

        inRangeOfCommander = false;
    }
}
