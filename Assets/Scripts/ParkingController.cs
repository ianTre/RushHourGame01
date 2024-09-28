using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkingController : MonoBehaviour
{
    // Start is called before the first frame update
    bool hasBeenTriggered = false;
    Player player;
    void OnTriggerStay2D(Collider2D other)
    {
        
        AudioSource successAudio;
        if(other.tag == "Player" )
        {
            VehicleController vehicleController = other.GetComponent<VehicleController>();
            player = vehicleController.player;
            if((float)player.Velocity == 0f && !hasBeenTriggered && player.GetHambugers() > 0)
            {
                hasBeenTriggered = true;
                successAudio = GetComponent<AudioSource>();
                successAudio.PlayDelayed(1);
                player.DecreaseHambugerCount();
                player.IncreaseScore(50);
                Invoke("RemoveParkingLot",3);
            }
        }
    }

    void RemoveParkingLot()
    {
        Destroy(this.gameObject);
    }
}
