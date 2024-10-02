using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class CanvasGameController : MonoBehaviour
{
    [SerializeField] Text scoreText;
    [SerializeField] Text HamburgerText;
    Player player = null;
    public float priceToUpdate;
    void Start()
    {
    }

    void Update()
    {
        if(player == null)
        {
            player = FindObjectOfType<VehicleController>().player;
        }   
        this.scoreText.text = this.scoreText.text.Replace("[Score]",player.Score().ToString());
        this.HamburgerText.text = ": " + player.GetHambugers().ToString();
        
    }

    
}
