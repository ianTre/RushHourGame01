public class Player
{
    public int Lives { get; set; }
    public int Score { get; set; }
    public float Velocity { get; set; }
    public float Acceleration { get; set; }
    
    public Player(float Acceleration)
    {
        this.Acceleration = Acceleration;
    }

    public void CalculateVelocity()
    {
        if (Velocity == 0.01f)
        {this.Velocity = Velocity;}
        else
        {this.Velocity += Acceleration;}

    }
}