using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelTankController : MonoBehaviour
{
    FuelController fuelController;
    private float refuelAmmount = 250;
    void Start()
    {
        fuelController = FindObjectOfType<FuelController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag !=  "Player")
            return;
        fuelController.Refuel(refuelAmmount);
        this.gameObject.SetActive(false);
    }
    
}
