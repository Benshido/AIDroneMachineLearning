using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DroneCollissionDetection : MonoBehaviour
{
    public new Rigidbody rigidbody;
    private Vector3 StartPosition;
    private Vector3 StartRotation;
    public bool collided = false;
    public GameObject CrashHitSource;
    public GameObject PropellerSource;
    public WinLoseStates WinAndLoseStateScript;
    public Mission_Manager MissionManager;
    public GameObject Finish;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        // Set starting postition and rotation for respawning
        StartPosition = transform.position;
        StartRotation = transform.localEulerAngles;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        // Check if the drone already crashed, to prevent double triggering
        if (!collided) 
        {
            if (!collision.gameObject.CompareTag("Cargo") && !collision.gameObject.CompareTag("Victim") && !collision.gameObject.CompareTag("Equipment") && !collision.gameObject.CompareTag("HomeBase"))
            {
                //Debug.Log("crash");
                Crash();
            }
            else if (collision.gameObject.CompareTag("HomeBase"))
            {
                WinAndLoseStateScript.Win();
                
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("burn");
        if (!collided && other.CompareTag("Fire"))
        {
            Crash();
            Burn();
        }
        
    }


    public void Crash()
    {
        Debug.Log("crash");
        collided = true;
        PropellerSource.GetComponent<PropellerAudioManager>().Crashed = true;
        CrashHitSource.GetComponent<CrashSoundManager>().PlayCrashSound();
        //PropellerSource.SetActive(false);
        //Debug.Log("Hit Object");
        //rigidbody.useGravity = true;
        gameObject.GetComponent<DroneController>().enabled = false;
        // Set delay for respawing
        MissionManager.CrashReset();
        Finish.SetActive(false);
        WinAndLoseStateScript.Lose();
        
    }

    public void Respawn()
    {
        // Recovering all starting states for respawning
        //rigidbody.useGravity=false;
        //PropellerSource.SetActive(true);
        UnBurn();
        
        PropellerSource.GetComponent<PropellerAudioManager>().Crashed = false;
        gameObject.GetComponent<DroneController>().enabled = true;
        this.gameObject.transform.position = StartPosition;
        this.gameObject.transform.localEulerAngles = StartRotation;
        collided = false;
    }
    public void Burn()
    {
        gameObject.transform.GetChild(1).gameObject.SetActive(true);
        
    }
    private void UnBurn()
    {
        gameObject.transform.GetChild(1).gameObject.SetActive(false);
    }

    
}
