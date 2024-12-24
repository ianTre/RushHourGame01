using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hambuger : MonoBehaviour
{
    private int Id;
    public bool hasBeingColored = false;
    public Color color;
    public float price;
    [SerializeField] float deadline;
    [SerializeField] float origialDistance;
    public bool visibleOnMap = false;
    public bool activeMission = false;
    public float timeToEndMission;
    public float timeToPickMission;
    public bool beingTracked; //Used for programming purpose only
    private GameObject hamburgerIcon;

    void Start()
    {
        
    }

    public void AssociateIcon(GameObject icon)
    {   
        this.hamburgerIcon = icon;
    }

    public GameObject GetIcon()
    {
        return this.hamburgerIcon;
    }

    void Update()
    {
        if(visibleOnMap)
            timeToPickMission = timeToPickMission - Time.deltaTime;

        if(activeMission)
        {
            timeToEndMission = timeToEndMission - Time.deltaTime;
            if(timeToEndMission <= 0f )
            {
                Debug.Log("Mission failed");
                //MISSION FAILED 
                BurgerController burgerController = this.gameObject.GetComponent<BurgerController>();
                burgerController.RemoveHambuger();
            }
        }   
        
    }


    public Transform getParkingLot()
    {
        return this.transform.Find("ParkingLot");
    }

}
