using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed_In_Kilimeter_a_Hour = 50.0f;
    [SerializeField] private float turnSpeed = 20f;
    [SerializeField] private float horizontalInput = 0f;
    [SerializeField] private float forwardInput = 0f;
    private float speed_Conversion_KMU_To_MS
    {
        get { return (5f / 18f) * speed_In_Kilimeter_a_Hour; }
        
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // here we get the player input
        forwardInput = Input.GetAxis("LeftVertical");
        horizontalInput = Input.GetAxis("LeftHorizontal");

        // here we move the Vehicle
        transform.Translate(Vector3.forward * Time.deltaTime * speed_Conversion_KMU_To_MS * forwardInput);
        transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed * horizontalInput);
    }

    

}
