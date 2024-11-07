using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OGDroneController : MonoBehaviour
{
    public new Rigidbody rigidbody;
    public float NegativeGravity = 490.5f;
    public float FrontSpeed = 0;
    public float SideSpeed = 0;
    public float MaxSpeed = 100;
    public float MaxLiftSpeed = 200;
    public float RotationAngle = 45;
    public float LiftSpeed = 0;
    public float TurnSpeed = 0;
    public float MaxTurnSpeed = 10;
    public float YRotationSpeed = 20;
    public float ElevationInput;
    public float YRotationInput;
    public float ForwardAccelerationInput;
    public float SidewaysAccelerationInput;
    public float FrontDirection;
    public float StickDeadZone = 0.3f;
    public float SpeedDelay = 0.5f;
    public GameObject FirstPersonCamera;
    public GameObject ThirddPersonCamera;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        // Setting the correct force according to the stick input
        rigidbody.AddRelativeForce(SideSpeed, (NegativeGravity * Time.deltaTime) + LiftSpeed, FrontSpeed);
        rigidbody.AddRelativeTorque(0, TurnSpeed, 0);

        // Setting the correct stick controls
        ElevationInput = Input.GetAxis("LeftVertical");
        YRotationInput = Input.GetAxis("LeftHorizontal");
        // Using translate to set elevation and rotation with the left stick
        if (ElevationInput > StickDeadZone)
        {
            LiftSpeed = MaxLiftSpeed * ElevationInput * Time.deltaTime;
            //LiftSpeed = Mathf.Lerp(LiftSpeed, MaxLiftSpeed * ElevationInput, Time.deltaTime);
        }
        else if (ElevationInput < -StickDeadZone)
        {
            LiftSpeed = MaxLiftSpeed * ElevationInput * Time.deltaTime;
            //LiftSpeed = Mathf.Lerp(LiftSpeed, MaxLiftSpeed * ElevationInput, Time.deltaTime);
        }
        else
        {
            LiftSpeed = 0;
        }

        if (YRotationInput > StickDeadZone)
        {
            TurnSpeed = MaxTurnSpeed * Time.deltaTime;
            //TurnSpeed = Mathf.Lerp(TurnSpeed, MaxTurnSpeed * YRotationInput, Time.deltaTime);
        }
        else if (YRotationInput < -StickDeadZone)
        {
            TurnSpeed = -MaxTurnSpeed * Time.deltaTime;
            //TurnSpeed = Mathf.Lerp(TurnSpeed, MaxTurnSpeed * YRotationInput, Time.deltaTime);
        }
        else
        {
            TurnSpeed = 0;
        }

        // Setting the correct stick input
        ForwardAccelerationInput = Input.GetAxis("RightVertical");
        // Setting the forwards and backwards speed for the right stick movement
        if (ForwardAccelerationInput < -StickDeadZone)
        {
            FrontSpeed = MaxSpeed * ForwardAccelerationInput * Time.deltaTime;
            //FrontSpeed = Mathf.Lerp(FrontSpeed, MaxSpeed * ForwardAccelerationInput, Time.deltaTime);
        }
        else if (ForwardAccelerationInput > StickDeadZone)
        {
            FrontSpeed = MaxSpeed * ForwardAccelerationInput * Time.deltaTime;
            //FrontSpeed = Mathf.Lerp(FrontSpeed, MaxSpeed * ForwardAccelerationInput, Time.deltaTime);
        }
        else
        {
            FrontSpeed = 0;
        }

        // Setting the correct stick input
        SidewaysAccelerationInput = Input.GetAxis("RightHorizontal");
        // Setting the sideways speed for the right stick movement
        if (SidewaysAccelerationInput > StickDeadZone)
        {
            SideSpeed = MaxSpeed * -SidewaysAccelerationInput * Time.deltaTime;
            //SideSpeed = Mathf.Lerp(SideSpeed, -MaxSpeed * SidewaysAccelerationInput, Time.deltaTime);
        }
        else if (SidewaysAccelerationInput < -StickDeadZone)
        {
            SideSpeed = MaxSpeed * -SidewaysAccelerationInput * Time.deltaTime;
            //SideSpeed = Mathf.Lerp(SideSpeed, -MaxSpeed * SidewaysAccelerationInput, Time.deltaTime);
        }
        else
        {
            SideSpeed = 0;
        }

        if ((Input.GetKeyUp(KeyCode.Joystick1Button3)))
        {
            if (FirstPersonCamera)
            {
                FirstPersonCamera.SetActive(false);
                ThirddPersonCamera.SetActive(true);
            }
            else
            {
                FirstPersonCamera.SetActive(true);
                ThirddPersonCamera.SetActive(false);
            }
        }

        Debug.Log(rigidbody.velocity);

    }
    public void gravity(bool hooked)
    {
        if (hooked)
        {
            NegativeGravity = 1013;
        }
        else
        {
            NegativeGravity = 490.5f;
        }
    }
}
