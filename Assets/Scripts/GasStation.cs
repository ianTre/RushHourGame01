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
    float refuelAmount = 10f;
    
    float fulltank = 1000;
    bool carInsideStation = false;
    AudioSource tankfilling;
    bool isfueltank = false;

    public void Start()
    {
        vehicleController =  FindObjectOfType<VehicleController>();
        fuelController = FindObjectOfType<FuelController>();

    }

    public void Update()
    {
        if(carInsideStation)
        {
            fuelController.Refuel(refuelAmount * Time.deltaTime, isfueltank);
                    
        }

    
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            carInsideStation = true;
            mainCamera.nearClipPlane = 30;
            int score = vehicleController.player.Score();
            if(vehicleController.player.fuellevel < 1000 && score > 0)
            {
            tankfilling = GetComponent<AudioSource>();
            tankfilling.Play();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player")
        { 
            
            if(vehicleController.player.fuellevel == 1000)
            {
                tankfilling = GetComponent<AudioSource>();
                tankfilling.Stop();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            carInsideStation = false;
            mainCamera.nearClipPlane = 0.01f;
            tankfilling = GetComponent<AudioSource>();
            tankfilling.Stop();
        }
    }
}
