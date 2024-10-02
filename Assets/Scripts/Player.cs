using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class Player
{
    public int Lives { get; set; }
    private int score = 0;
    public float Velocity { get; set; }
    private float AccelerationRate { get; set; }
    public float Accelaration { get; set; }
    public float maxVelocity = 25f;
    private float reverseMaxVelocity = -10f;
    private float slowVelocityBias = 0.5f;
    private int HamburgerCount = 0;
    List<BurgerController> hamburgers;
    
    public Player(float AccelerationRate)
    {
        this.AccelerationRate = AccelerationRate;
    }

    /// <summary>
    /// Calculate new velocity.
    /// </summary>
    /// <param name="gradient">Gradient to know direction of acceleration force. Param can be 
    /// 1 : Accelerate
    /// 0 : No Accelerate ( friction force will apply to acceleration)
    /// -1: Negative Accelerate ( can be break or reverse)
    /// </param>
    public void CalculateAcceleration(AccelerationGradient gradient)
    {
        if(gradient == AccelerationGradient.Accelerate) //Accelerate
        {
            Accelaration = AccelerationRate;
            if (Velocity < maxVelocity) //Max velocity
                this.Velocity += Accelaration;
        }

        if(gradient == AccelerationGradient.FastAccelerate)
        {
            if(this.Velocity > 0 && this.Velocity < maxVelocity*2)
            {
                this.Velocity = Velocity * 1.01f;
                this.Velocity += Accelaration;
            }
        }

        if(gradient == AccelerationGradient.None) //
        {
            this.Accelaration = 0;
            if(this.Velocity > 0)
            {
                this.Velocity = this.Velocity  * 0.995f;
            }
            
            if( this.Velocity < slowVelocityBias)
            {
                this.Velocity = 0;
            }
        }

        if(gradient == AccelerationGradient.Deccelerate)
        {
            Accelaration = AccelerationRate;
            if(Velocity > 0)
            {
                this.Velocity -= Accelaration;
            }
            else
            {
                if(Velocity > reverseMaxVelocity)
                    this.Velocity -= Accelaration;
            }
        }

        if (gradient == AccelerationGradient.HandBreake)
        {
            if (Velocity > 0)
            {
                Accelaration = AccelerationRate * 2;
                if (Velocity < maxVelocity) //Max velocity
                    this.Velocity -= Accelaration;
            }
        }
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

}

public enum AccelerationGradient
{
    None = 0,
    Accelerate = 1 , 
    Deccelerate = -1,
    FastAccelerate = 2 ,
    HandBreake = -2,
}