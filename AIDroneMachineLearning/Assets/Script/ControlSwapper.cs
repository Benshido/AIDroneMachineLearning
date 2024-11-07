using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSwapper : MonoBehaviour
{
    public GameObject Drone;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            if (Drone.GetComponent<DroneController>().enabled == true)
            {
                Drone.GetComponent<DroneController>().enabled = false;
                Drone.GetComponent<OGDroneController>().enabled = true;
            }
            else
            {
                Drone.GetComponent<DroneController>().enabled = true;
                Drone.GetComponent<OGDroneController>().enabled = false;
            }
        }
    }
}
