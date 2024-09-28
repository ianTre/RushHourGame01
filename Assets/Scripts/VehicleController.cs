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
    private bool upArrowKeyPressed;
    private bool upArrowKeyReleased;

    // Start is called before the first frame update
    void Start()
    {
        myTransform = this.GetComponent<Transform>();
        player = new Player(Initialacceleration);
        accelerateSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
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
            Invoke("StopCarSound", 0.1f);
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
    }

    private void CalculateAccelerateSoundVolume()
    {
        float soundFraction = player.Velocity / player.maxVelocity;
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
}
