using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class Player
{
    public int Lives { get; set; }
    private int score = 0;
    public float VelocityRate { get; set; }
    public float actualSpeed;
    public float Accelaration { get; set; }
    public float maxVelocity = 50f;
    public float minVelocity = 20f;
    private float reverseMaxVelocity = 10f;
    private int HamburgerCount = 0;
    List<BurgerController> hamburgers;
    public float fuellevel;
    
    public Player(float AccelerationRate)
    {
        this.Accelaration = AccelerationRate;
        this.VelocityRate = minVelocity;
    }

    /// Calculate new velocity.
    public float CalculateSpeed(AccelerationGradient gradient)
    {
        if(gradient == AccelerationGradient.Accelerate) //Accelerate
        {
            if (VelocityRate < maxVelocity) //Max velocity
                this.VelocityRate += Accelaration;
            else
            {
                this.VelocityRate -= Accelaration;
            }
        }

        if(gradient == AccelerationGradient.FastAccelerate)
        {
            Console.Write("Fast Accelerate !!");
            if(this.VelocityRate < maxVelocity*2)
            {
                this.VelocityRate += Accelaration * 1.5f;
            }
        }

        if(gradient == AccelerationGradient.None) 
        {
            if(this.VelocityRate > minVelocity)
            {
                this.VelocityRate--;
                Console.Write("slow down WR !!");
            }
                
        }

        if(gradient == AccelerationGradient.Deccelerate)
        {
            this.VelocityRate = reverseMaxVelocity;
        }

        if (gradient == AccelerationGradient.HandBreake)
        {
            Console.Write("Hand breake !!");
            if (VelocityRate > 0)
            {
                VelocityRate = VelocityRate/2;
            }
        }

        return this.VelocityRate;
    }

    public int GetHambugers()
    {
        return this.HamburgerCount;
    }

    public void IncreaseHambugerCount()
    {
        this.HamburgerCount++;
    }

    public void DecreaseHambugerCount()
    {
        this.HamburgerCount--;
    }

    public int Score()
    {
        return this.score;
    }
   
    public void IncreaseScore(int amount)
    {
        this.score = this.score + amount;
    }
    public void DecreaseScore(int amount)
    {
        this.score = this.score - amount;
    }

}

public enum AccelerationGradient
{
    None = 0,
    Accelerate = 1 , 
    Deccelerate = -1,
    FastAccelerate = 2 ,
    HandBreake = -2,
}