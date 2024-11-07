using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashSoundManager : MonoBehaviour
{
    public AudioSource CrashHitSource;
    public AudioClip CrashClip;
    public GameObject DroneController;
    public bool Crashed = false;
    public bool IsCrashed = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    

    public void PlayCrashSound()
    {
        CrashHitSource.PlayOneShot(CrashClip);
    }
}
