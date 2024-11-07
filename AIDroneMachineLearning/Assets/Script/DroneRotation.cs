using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DroneRotation : MonoBehaviour
{
    public float FrontRotationSpeed = 10;
    public float SideRotationSpeed = 10;
    public float RotationAngle = 45;
    public float LiftSpeed = 10;
    public float YRotationSpeed = 20;
    public float ElevationInput;
    public float YRotationInput;
    public float ForwardAccelerationInput;
    public float SidewaysAccelerationInput;

    public float angle = 25;
    public float ZAngle = 0;
    public float XAngle = 0;
    public bool Banking = true;
    
    // Update is called once per frame
    void Update()
    {
        if (Banking)
        {
            // Setting the rotation after moving the drone
            transform.localEulerAngles = Vector3.back * XAngle + Vector3.right * ZAngle;
            // Setting correct stick inputs
            ForwardAccelerationInput = Input.GetAxis("RightVertical");
            SidewaysAccelerationInput = Input.GetAxis("RightHorizontal");

            // Setting the rotation according to stick movement
            if (ForwardAccelerationInput < -0.3)
            {
                ZAngle = Mathf.Lerp(ZAngle, -angle, Time.deltaTime);
            }
            else if (ForwardAccelerationInput > 0.3)
            {
                ZAngle = Mathf.Lerp(ZAngle, angle, Time.deltaTime);
            }
            else
            {
                ZAngle = Mathf.Lerp(ZAngle, 0, Time.deltaTime);
            }

            if (SidewaysAccelerationInput < -0.3)
            {
                XAngle = Mathf.Lerp(XAngle, angle, Time.deltaTime);
            }
            else if (SidewaysAccelerationInput > 0.3)
            {
                XAngle = Mathf.Lerp(XAngle, -angle, Time.deltaTime);
            }
            else
            {
                XAngle = Mathf.Lerp(XAngle, 0, Time.deltaTime);
            }
        }

        else
        {
            transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            if (Banking)
            {
                Banking = false;
            }
            else
            {
                Banking = true;
            }
        }
        
    }
}
