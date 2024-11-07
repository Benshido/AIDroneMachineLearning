using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PropellerAudioManager : MonoBehaviour
{
    public AudioSource PropellerSource;
    public GameObject DroneController;
    public float ElevationInput;
    public float YRotationInput;
    public float ForwardAccelerationInput;
    public float SidewaysAccelerationInput;
    public float StickDeadZone;
    public float[] Inputs = new float[4];
    public float MinSoundVolume = 0.2f;
    public float MaxSoundVolume = 0.6f;
    public float MinSoundPitch = 1;
    public float MaxSoundPitch = 1.1f;
    public bool Crashed = false;
    // Start is called before the first frame update
    void Start()
    {
        StickDeadZone = DroneController.GetComponent<DroneController>().StickDeadZone;
    }

    // Update is called once per frame
    void Update()
    {
        GetInputValues();
        Inputs[0] = ElevationInput;
        Inputs[1] = YRotationInput;
        Inputs[2] = ForwardAccelerationInput;
        Inputs[3] = SidewaysAccelerationInput;

        if (Inputs.Any(v => v > StickDeadZone || v < -StickDeadZone) && !Crashed)
        {
            PropellerSource.volume = Mathf.Lerp(PropellerSource.volume, MaxSoundVolume, Time.deltaTime * 5);
            PropellerSource.pitch = Mathf.Lerp(PropellerSource.pitch, MaxSoundPitch, Time.deltaTime * 5);
        }
        if (Inputs.All(v => v < StickDeadZone || v > -StickDeadZone) && !Crashed)
        {
            PropellerSource.volume = Mathf.Lerp(PropellerSource.volume, MinSoundVolume, Time.deltaTime * 5);
            PropellerSource.pitch = Mathf.Lerp(PropellerSource.pitch, MinSoundPitch, Time.deltaTime * 5);
        }

        if (SidewaysAccelerationInput > StickDeadZone && !Crashed)
        {
            PropellerSource.panStereo = Mathf.Lerp(PropellerSource.panStereo, 0.2f, Time.deltaTime * 5);
        }
        else if (SidewaysAccelerationInput < -StickDeadZone && !Crashed)
        {
            PropellerSource.panStereo = Mathf.Lerp(PropellerSource.panStereo, -0.2f, Time.deltaTime * 5);
        }
        else
        {
            PropellerSource.panStereo = Mathf.Lerp(PropellerSource.panStereo, 0, Time.deltaTime * 5);
        }

        if (Crashed)
        {
            PropellerSource.volume = Mathf.Lerp(PropellerSource.volume, 0, Time.deltaTime * 5);
            PropellerSource.pitch = Mathf.Lerp(PropellerSource.pitch, 0.8f, Time.deltaTime * 5);
        }
    }

    public void GetInputValues()
    {
        ElevationInput = DroneController.GetComponent<DroneController>().ElevationInput;
        YRotationInput = DroneController.GetComponent<DroneController>().YRotationInput;
        ForwardAccelerationInput = DroneController.GetComponent<DroneController>().ForwardAccelerationInput;
        SidewaysAccelerationInput = DroneController.GetComponent<DroneController>().SidewaysAccelerationInput;
    }
}
