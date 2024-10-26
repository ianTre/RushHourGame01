using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkingController : MonoBehaviour
{
    // Start is called before the first frame update
    bool hasBeenTriggered = false;
    Player player;
    public BurgerController burgerController;
    void OnTriggerStay2D(Collider2D other)
    {
        //MISSION SUCCES
        AudioSource successAudio;
        if(other.tag == "Player" )
        {
            VehicleController vehicleController = other.GetComponent<VehicleController>();
            player = vehicleController.player;
            if((float)player.actualSpeed == 0f && !hasBeenTriggered && player.GetHambugers() > 0)
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
        burgerController = this.transform.parent.GetComponent<BurgerController>();
        burgerController.RemoveHambuger();
        Destroy(this.gameObject);
    }
}
