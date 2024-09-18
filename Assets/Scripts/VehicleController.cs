using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class VehicleController : MonoBehaviour
{
    public Transform myTransform;
    Player player ;
    [SerializeField] float acceleration;
    [SerializeField] float velocity;
    [SerializeField] float rotationSpeed;
    [SerializeField] ParticleSystem smoke;
    private bool isSmokeActive = false;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("I am alive now");
        myTransform = this.GetComponent<Transform>();
        player = new Player(0.001f);
    }

    // Update is called once per frame
    void Update()
    {
        AccelerationGradient ActualGradient = AccelerationGradient.None;
        
        if(Input.GetAxis("Vertical") == 1)
        {
             ActualGradient = AccelerationGradient.Accelerate;
        }

        if(Input.GetKey(KeyCode.LeftShift))
        {
            ActualGradient = AccelerationGradient.FastAccelerate;
            
            if(!isSmokeActive)
            {
                smoke.Play();
                Invoke("StopSmoke",5);
                this.isSmokeActive = true;
            }
            
        }

         if(Input.GetAxis("Vertical") == -1)
        {
            ActualGradient = AccelerationGradient.Deccelerate;
        }

          if(Input.GetAxis("Horizontal") == 1)
        {
            if(this.player.Velocity > 0)
                this.transform.Rotate(0,0,-1 * rotationSpeed);
        }

          if(Input.GetAxis("Horizontal") == -1)
        {
            if(this.player.Velocity > 0)
                this.transform.Rotate(0,0,rotationSpeed);
        }

        player.CalculateAcceleration(ActualGradient);
        this.acceleration = player.Accelaration;
        this.velocity = player.Velocity;
        //this.myTransform.position = new Vector3(myTransform.position.x , myTransform.position.y + player.Velocity);
        Vector3 vector3 = new Vector3(0,player.Velocity,0);
        this.myTransform.Translate(vector3);
    }


    private void StopSmoke()
    {
        this.isSmokeActive = false;
        smoke.Stop();
    }
}
