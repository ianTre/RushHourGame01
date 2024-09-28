using System;
using System.ComponentModel;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class VehicleController : MonoBehaviour
{
    AudioSource Accelerate; 
    public Transform myTransform;
    public Player player ;
    [SerializeField] float Initialacceleration;
    [SerializeField] float velocity;
    [SerializeField] float rotationSpeed;
    [SerializeField] ParticleSystem smoke;

    private bool isSmokeActive = false;
    private bool isCarsoundActive = false;
    private bool keypressing;
    private bool keyunpressing;

    // Start is called before the first frame update
    void Start()
    {
        myTransform = this.GetComponent<Transform>();
        player = new Player(Initialacceleration);
    }

    // Update is called once per frame
    void Update()
    {
        AccelerationGradient ActualGradient = AccelerationGradient.None;
        
        keypressing = Input.GetKeyDown(KeyCode.UpArrow);
        keyunpressing = Input.GetKeyUp(KeyCode.UpArrow);
         
        if (keypressing && !isCarsoundActive)
        {
            Accelerate = GetComponent<AudioSource>();
            Accelerate.Play();
            this.isCarsoundActive = true;

        }

        if (keyunpressing && isCarsoundActive)
        {
            Accelerate = GetComponent<AudioSource>();
            Invoke("StopCarSound", 2);

        }

        if (Input.GetAxis("Vertical") == 1)
        {
            ActualGradient = AccelerationGradient.Accelerate;
         
        }

        if(Input.GetKey(KeyCode.LeftShift) && Input.GetAxis("Vertical") == 1)
        {
            ActualGradient = AccelerationGradient.FastAccelerate;
            
            if(!isSmokeActive)
            {
                smoke.Play();
                Invoke("StopSmoke",2);
                this.isSmokeActive = true;
            }
        }
        
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetAxis("Vertical") == -1)
        {
            ActualGradient = AccelerationGradient.FastAccelerate;
           
        }

        if (Input.GetAxis("Vertical") == -1)
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

        if (Input.GetKey(KeyCode.Space) )
        {
            ActualGradient = AccelerationGradient.HandBreake;
       
        }

        player.CalculateAcceleration(ActualGradient);
        this.velocity = player.Velocity;
        //this.myTransform.position = new Vector3(myTransform.position.x , myTransform.position.y + player.Velocity);
        Vector3 vector3 = new Vector3(0,velocity * Time.deltaTime,0);
        this.myTransform.Translate(vector3);
    }


    private void StopSmoke()
    {
        this.isSmokeActive = false;
        smoke.Stop();
    }
    private void StopCarSound()
    {
        Accelerate.SetScheduledEndTime(velocity);
        this.isCarsoundActive = false;


    }
}
