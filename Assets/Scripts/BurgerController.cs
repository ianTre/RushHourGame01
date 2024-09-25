using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BurgerController : MonoBehaviour
{
    AudioSource successAudio;
    SpriteRenderer spriteRenderer;
    [SerializeField] GameObject player;
    bool hasBeenTriggered=false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player" )
        {
            VehicleController vehicleController = other.GetComponent<VehicleController>();
            if((float)vehicleController.player.Velocity == 0f && !hasBeenTriggered)
            {
                hasBeenTriggered = true;
                successAudio = GetComponent<AudioSource>();
                successAudio.Play();
                spriteRenderer = GetComponent<SpriteRenderer>();
                spriteRenderer.color = Color.green;
                Invoke("RemoveHambuger",3);
            }
        }
    }


    private void RemoveHambuger()
    {
        Destroy(this.gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        
    }
}
