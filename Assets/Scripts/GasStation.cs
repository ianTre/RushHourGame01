using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GasStation : MonoBehaviour
{
    [SerializeField]
    Camera mainCamera;
    VehicleController vehicleController;
    FuelController fuelController;
    Player player; 
    float refuelAmount = 1;
    float fulltank = 1000;
    bool carInsideStation = false;

    public void Start()
    {
        vehicleController =  FindObjectOfType<VehicleController>();
        fuelController = FindObjectOfType<FuelController>();
    }

    public void Update()
    {
        if(carInsideStation)
            fuelController.Refuel(refuelAmount);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            carInsideStation = true;
            mainCamera.nearClipPlane = 30;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("Hey i am here");
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            carInsideStation = false;
            mainCamera.nearClipPlane = 0.5f;
        }
    }
}
