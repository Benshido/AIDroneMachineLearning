using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DynamicPositionLables : MonoBehaviour
{
    public VisualElement hud;
    public Label height_lable;
    public Label speed_lable;

    public GameObject drone;
    private double currentHeight = 0;
    private double currentSpeed = 0;


    private void Awake()
    {
        hud = GetComponent<UIDocument>().rootVisualElement;
    }

    private void OnEnable()
    {
        height_lable = hud.Q<Label>("Height_Variable");
        speed_lable = hud.Q<Label>("Velocity_Variable");
        
    }

    private void Update()
    {
        if (drone != null)
        {
            currentHeight = Math.Round(drone.transform.position.y, 1);
            height_lable.text = currentHeight.ToString();

            currentSpeed = Math.Round(drone.GetComponent<Rigidbody>().velocity.magnitude,1);
            speed_lable.text = currentSpeed.ToString(); 
        }
    }
}
