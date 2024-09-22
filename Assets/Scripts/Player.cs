public class Player
{
    public int Lives { get; set; }
    public int Score { get; set; }
    public float Velocity { get; set; }
    private float AccelerationRate { get; set; }
    public float Accelaration { get; set; }
    private float maxVelocity = 2f;
    private float slowVelocityBias = 0.1f;

    
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
            if(this.Velocity < 0.1)
            {
                this.Velocity = 0.3f;
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
            if (Velocity < maxVelocity) //Max velocity
                this.Velocity -= Accelaration;
        }
        CalculateVelocity();
    }

    public void CalculateVelocity()
    {
        
        
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