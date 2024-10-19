using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.PlasticSCM.Editor;
using Unity.VisualScripting;
using UnityEngine;

public class BurgerController : MonoBehaviour
{
    AudioSource successAudio;
    SpriteRenderer spriteRenderer;
    [SerializeField] GameObject player;
    bool hasBeenTriggered=false;
    VehicleController vehicleController;
    GameObject parkingLotGameObject;
    public Hambuger hamburger;
    private HambugerSpawnerController hambugerSpawnerController;
    private void OnTriggerEnter2D(Collider2D other)
    {
        
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player" )
        {
            vehicleController = other.GetComponent<VehicleController>();
            if((float)vehicleController.player.Velocity == 0f && !hasBeenTriggered)
            {
                hasBeenTriggered = true;
                successAudio = GetComponent<AudioSource>();
                successAudio.Play();
                AsssignNewMission();
                Invoke("HideHamburger",5);
            }
        }
    }

    //YOU PICK AN HAMBURGER
    private void AsssignNewMission()
    {
        hambugerSpawnerController = FindObjectOfType<HambugerSpawnerController>();
        hambugerSpawnerController.activeMissions++;
        spriteRenderer = GetComponent<SpriteRenderer>();
        hamburger = this.gameObject.GetComponent<Hambuger>();
        spriteRenderer.color = hamburger.color;
        hamburger.timeToEndMission = 60;
        hamburger.activeMission = true; 
        vehicleController.player.IncreaseHambugerCount();
        parkingLotGameObject = this.gameObject.transform.GetChild(0).gameObject;
        parkingLotGameObject.SetActive(true);
        parkingLotGameObject.GetComponent<SpriteRenderer>().color = hamburger.color;
    }



    public void RemoveHambuger()
    {
        hambugerSpawnerController.returnColor(this.hamburger.color);
        FindObjectOfType<CanvasGameController>().removeHamburger(this.hamburger);
        Destroy(this.gameObject);
    }

    public void HideHamburger()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        
    }
}
