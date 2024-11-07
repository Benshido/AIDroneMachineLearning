using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DroneController : MonoBehaviour
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
    public float LiftDeadZone = 0.1f;
    public float TurnDeadZone = 0.1f;
    public float SpeedDelay = 0.5f;
    public GameObject FirstPersonCamera;
    public GameObject ThirddPersonCamera;

    private Vector3 lastPosition; // To store the last position for speed calculation
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        // Setting the correct force according to the stick input
        rigidbody.AddRelativeForce(SideSpeed * Time.deltaTime, (NegativeGravity * Time.deltaTime) + LiftSpeed * Time.deltaTime, FrontSpeed * Time.deltaTime);
        rigidbody.AddRelativeTorque(0, TurnSpeed * Time.deltaTime, 0);

        // Setting the correct stick controls
        ElevationInput = Input.GetAxis("LeftVertical");
        YRotationInput = Input.GetAxis("LeftHorizontal");
        // Using translate to set elevation and rotation with the left stick
        if (ElevationInput > LiftDeadZone)
        {
            //LiftSpeed = MaxLiftSpeed * ElevationInput * Time.deltaTime;
            LiftSpeed = Mathf.Lerp(LiftSpeed, MaxLiftSpeed * ElevationInput, Time.deltaTime);
            TurnDeadZone = 0.5f;
        }
        else if (ElevationInput < -LiftDeadZone)
        {
            //LiftSpeed = MaxLiftSpeed * ElevationInput * Time.deltaTime;
            LiftSpeed = Mathf.Lerp(LiftSpeed, MaxLiftSpeed * ElevationInput, Time.deltaTime);
            TurnDeadZone = 0.5f;
        }
        else
        {
            LiftSpeed = 0;
            TurnDeadZone = 0.1f;
        }

        if (YRotationInput > TurnDeadZone)
        {
            TurnSpeed = MaxTurnSpeed * YRotationInput * Time.deltaTime;
            //TurnSpeed = Mathf.Lerp(TurnSpeed, MaxTurnSpeed * YRotationInput, Time.deltaTime * 2);
            LiftDeadZone = 0.5f;
        }
        else if (YRotationInput < -TurnDeadZone)
        {
            TurnSpeed = MaxTurnSpeed * YRotationInput * Time.deltaTime;
            //TurnSpeed = Mathf.Lerp(TurnSpeed, MaxTurnSpeed * YRotationInput, Time.deltaTime);
            LiftDeadZone = 0.5f;
        }
        else
        {
            TurnSpeed = 0;
            LiftDeadZone = 0.1f;
        }

        // Setting the correct stick input
        ForwardAccelerationInput = Input.GetAxis("RightVertical");
        // Setting the forwards and backwards speed for the right stick movement
        if (ForwardAccelerationInput < -StickDeadZone)
        {
            //FrontSpeed = MaxSpeed * ForwardAccelerationInput * Time.deltaTime;
            FrontSpeed = Mathf.Lerp(FrontSpeed, MaxSpeed * ForwardAccelerationInput, Time.deltaTime);
        }
        else if (ForwardAccelerationInput > StickDeadZone)
        {
            //FrontSpeed = MaxSpeed * ForwardAccelerationInput * Time.deltaTime;
            FrontSpeed = Mathf.Lerp(FrontSpeed, MaxSpeed * ForwardAccelerationInput, Time.deltaTime);
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
            //SideSpeed = MaxSpeed * -SidewaysAccelerationInput * Time.deltaTime;
            SideSpeed = Mathf.Lerp(SideSpeed, -MaxSpeed * SidewaysAccelerationInput, Time.deltaTime);
        }
        else if (SidewaysAccelerationInput < -StickDeadZone)
        {
            //SideSpeed = MaxSpeed * -SidewaysAccelerationInput * Time.deltaTime;
            SideSpeed = Mathf.Lerp(SideSpeed, -MaxSpeed * SidewaysAccelerationInput, Time.deltaTime);
        }
        else
        {
            SideSpeed = 0;
        }

        //if ((Input.GetKeyUp(KeyCode.Joystick1Button3)))
        //{
        //    if (FirstPersonCamera)
        //    {
        //        FirstPersonCamera.SetActive(false);
        //        ThirddPersonCamera.SetActive(true);
        //    }
        //    else
        //    {
        //        FirstPersonCamera.SetActive(true);
        //        ThirddPersonCamera.SetActive(false);
        //    }
        //}

        float distanceTravelled = Vector3.Distance(lastPosition, transform.position);
        float actualSpeed = distanceTravelled / Time.deltaTime; // Speed in meters per second

        // Log the actual speed
        Debug.Log($"Actual Speed drone: {actualSpeed} m/s");

        // Update last position
        lastPosition = transform.position;


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
