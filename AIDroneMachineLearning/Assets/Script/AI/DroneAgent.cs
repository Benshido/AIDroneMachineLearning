using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class DroneAgent : Agent
{
    [SerializeField] private Transform targetTransform;
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

    private Vector3 lastPosition;
    public override void OnEpisodeBegin()
    {
        rigidbody = GetComponent<Rigidbody>();
        transform.localPosition = Vector3.zero;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(targetTransform.localPosition);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float elevationInput = actions.ContinuousActions[0];
        float yRotationInput = actions.ContinuousActions[1];
        float forwardAccelerationInput = actions.ContinuousActions[2];
        float sidewaysAccelerationInput = actions.ContinuousActions[3];

        ManeuverDrone(elevationInput, yRotationInput, forwardAccelerationInput, sidewaysAccelerationInput);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxis("LeftVertical");
        continuousActions[1] = Input.GetAxis("LeftHorizontal");
        continuousActions[2] = Input.GetAxis("RightVertical");
        continuousActions[3] = Input.GetAxis("RightHorizontal");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Goal>(out Goal goal))
        {
            SetReward(+1f);
            EndEpisode();
        }
        if (other.TryGetComponent<Wall>(out Wall wall))
        {
            SetReward(-1f);
            EndEpisode();
        }
    }

    public void ManeuverDrone(float m_elevationInput, float m_yRotationInput, float m_forwardAccelerationInput, float m_sidewaysAccelerationInput)
    {
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        // Setting the correct force according to the stick input
        rigidbody.AddRelativeForce(SideSpeed * Time.deltaTime, (NegativeGravity * Time.deltaTime) + LiftSpeed * Time.deltaTime, FrontSpeed * Time.deltaTime);
        rigidbody.AddRelativeTorque(0, TurnSpeed * Time.deltaTime, 0);

        // Setting the correct stick controls
        ElevationInput = m_elevationInput;
        YRotationInput = m_yRotationInput;
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
        ForwardAccelerationInput = m_forwardAccelerationInput;
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
        SidewaysAccelerationInput = m_sidewaysAccelerationInput;
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

        float distanceTravelled = Vector3.Distance(lastPosition, transform.position);
        float actualSpeed = distanceTravelled / Time.deltaTime; // Speed in meters per second

        // Log the actual speed
        Debug.Log($"Actual Speed drone: {actualSpeed} m/s");

        // Update last position
        lastPosition = transform.position;
    }
}
