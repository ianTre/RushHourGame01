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
    CarSoundsController carSoundsController;
    public Transform myTransform;
    public Player player ;
    [SerializeField] float Initialacceleration;
    [SerializeField] float velocity;
    [SerializeField] float rotationSpeed;
    [SerializeField] ParticleSystem smoke;
    public float deleteMe;

    private bool isSmokeActive = false;

    // Start is called before the first frame update
    void Start()
    {
        myTransform = this.GetComponent<Transform>();
        player = new Player(Initialacceleration);
        carSoundsController = GetComponent<CarSoundsController>();
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
        CalculateAccelerateSoundVolume(moveAmount,verticalInput);
        if(player.fuellevel <= 0f)   
        {
            carSoundsController.StopAllSounds();
            StopSmoke();
            moveAmount = moveAmount /8 ;
        }   
        player.actualSpeed = moveAmount;
        deleteMe = player.fuellevel;
        
        transform.Rotate(0,0,-steerAmount);
        transform.Translate(0,moveAmount,0);
        
    }
    

    private void CalculateAccelerateSoundVolume(float moveAmount,float verticualInput)
    {
        if(player.fuellevel <= 0)
            return;

        if (moveAmount > 0 )
        {
            carSoundsController.ReproduceMovingSound(verticualInput);
        }
        else
        {
            carSoundsController.ReproduceStandingSound();
        }
    }


    private void StopSmoke()
    {
        this.isSmokeActive = false;
        smoke.Stop();
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
