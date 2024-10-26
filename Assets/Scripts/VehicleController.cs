using System;
using System.ComponentModel;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class VehicleController : MonoBehaviour
{
    AudioSource accelerateSound; 
    public Transform myTransform;
    public Player player ;
    [SerializeField] float Initialacceleration;
    [SerializeField] float velocity;
    [SerializeField] float rotationSpeed;
    [SerializeField] ParticleSystem smoke;

    private bool isSmokeActive = false;
    private bool isCarsoundActive = false;

    // Start is called before the first frame update
    void Start()
    {
        myTransform = this.GetComponent<Transform>();
        player = new Player(Initialacceleration);
        accelerateSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        float fixedTime = Time.deltaTime;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        AccelerationGradient actualGradient = CalculateGradient(horizontalInput , verticalInput);

        this.velocity = player.CalculateSpeed(actualGradient);

        float steerAmount = horizontalInput * rotationSpeed * fixedTime;
        float moveAmount = verticalInput  * velocity * fixedTime;
        player.actualSpeed = moveAmount;

        CalculateAccelerateSoundVolume(moveAmount);
        transform.Rotate(0,0,-steerAmount);
        transform.Translate(0,moveAmount,0);
    }
    // Max: -14,7 - Min: 174.55
    // Update is called once per frame
    /*
    void Update()
    {
        AccelerationGradient ActualGradient = AccelerationGradient.None;
        
        upArrowKeyPressed = Input.GetKeyDown(KeyCode.UpArrow);
        upArrowKeyReleased = Input.GetKeyUp(KeyCode.UpArrow);
         
        if (upArrowKeyPressed && !isCarsoundActive)
        {
            accelerateSound.Play();
            this.isCarsoundActive = true;
        }

        if (upArrowKeyReleased && isCarsoundActive)
        {
            
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
            if(this.player.Velocity != 0)
                this.transform.Rotate(0,0,-1 * rotationSpeed);
        }

          if(Input.GetAxis("Horizontal") == -1)
        {
            if(this.player.Velocity != 0)
                this.transform.Rotate(0,0,rotationSpeed);
        }

        if (Input.GetKey(KeyCode.Space) )
        {
            ActualGradient = AccelerationGradient.HandBreake;
       
        }

        //Accelerate
        player.CalculateAcceleration(ActualGradient);
        this.velocity = player.Velocity;
        //Accelerate sound
        CalculateAccelerateSoundVolume();
    
        //Player movement
        Vector3 vector3 = new Vector3(0,velocity * Time.deltaTime,0);
        this.myTransform.Translate(vector3);
    }*/

    private void CalculateAccelerateSoundVolume(float moveAmount)
    {
        if(moveAmount > 0)
        {
            if(!isCarsoundActive)
            {
                accelerateSound.Play();
                this.isCarsoundActive = true;
            }
        }
        else
        {
            Invoke("StopCarSound", 0.1f);
        }

        float soundFraction =  (player.VelocityRate - player.minVelocity) / player.maxVelocity  / 2;
        accelerateSound.volume = soundFraction;
        if(soundFraction == 0 && !isCarsoundActive)
            accelerateSound.Stop();
    }

    private void StopSmoke()
    {
        this.isSmokeActive = false;
        smoke.Stop();
    }
    private void StopCarSound()
    {
        this.isCarsoundActive = false;
    }

    private AccelerationGradient CalculateGradient(float horizontalInput , float verticalInput)
    {
        AccelerationGradient gradient = AccelerationGradient.None;
        if(verticalInput > 0 )
            gradient = AccelerationGradient.Accelerate;

        if(verticalInput < 0)
            gradient = AccelerationGradient.Deccelerate;

        if(Input.GetKey(KeyCode.LeftShift) && verticalInput > 0)
        {
            gradient = AccelerationGradient.FastAccelerate;
            if(!isSmokeActive)
            {
                smoke.Play();
                Invoke("StopSmoke",2);
                this.isSmokeActive = true;
            }
        }

        if (Input.GetKey(KeyCode.Space) )
            gradient = AccelerationGradient.HandBreake;

        return gradient;
    }
}
