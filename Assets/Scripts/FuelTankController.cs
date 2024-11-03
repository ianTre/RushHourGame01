using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelTankController : MonoBehaviour
{
    FuelController fuelController;
    bool isfueltank = false;
    private float refuelAmmount = 250;
    void Start()
    {
        fuelController = FindObjectOfType<FuelController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag !=  "Player")
        {
            return;
        }
        else
        {
        isfueltank = true;
        fuelController.Refuel(refuelAmmount, isfueltank);
        this.gameObject.SetActive(false);
        }
    }
    
}
