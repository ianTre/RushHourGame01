using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class FuelController : MonoBehaviour
{
    Player player; 
    float fulltank = 1000;
    [SerializeField] float fueltest;

    int playerscore;
    int scoreFloatToInt;
    // Start is called before the first frame update
    //Max: -17 - Min: 176.83
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null)
        {
            player = FindObjectOfType<VehicleController>().player;
            player.fuellevel = 1000;
        }        
   
        if(player.fuellevel >=0)
        {
            if(player.actualSpeed > 0) //Going forward
            {
                player.fuellevel = player.fuellevel - (player.actualSpeed/3);
            }
            else if(player.actualSpeed < 0) //Going backwards
            {
                
                player.fuellevel = player.fuellevel + (player.actualSpeed/3);
            }
        
        Vector3 Zrotation = new Vector3(0,0,0);
        Zrotation.z = FuelconversionfromfolattoZrotation(player.fuellevel);
        this.transform.eulerAngles = Zrotation;
        
        }
    }

    public void Refuel(float refuelAmount, bool isfueltank)
    {   
        if(!isfueltank)
        {
            playerscore = player.Score();
            
            if(playerscore > 0)
            {
                this.player.fuellevel += refuelAmount;
                scoreFloatToInt = (int)refuelAmount;
                this.player.DecreaseScore(scoreFloatToInt);
                if(this.player.fuellevel >= fulltank) 
                {
                    this.player.fuellevel = fulltank;
                } 
            }
        }
        else
        {
           this.player.fuellevel += refuelAmount;
           if(this.player.fuellevel >= fulltank) 
           {
                this.player.fuellevel = fulltank;
           } 

        }
    }

    private float FuelconversionfromfolattoZrotation(float fuellevel)
    {
       //Max: -17 - Min: 176.83 range: -193

        float result;
        float fuelvariable;
        float minlevel = 176;
        float maxLevel = -17;
        float range = maxLevel - minlevel;

        fuelvariable = (fuellevel * 1) /fulltank;
        result = minlevel + (range*fuelvariable);
        fueltest = result;
        return result;
    }
}
