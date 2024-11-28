using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UIElements;

public class MainCameraControl : MonoBehaviour
{
    [SerializeField] GameObject player;
    Transform playerTransform;
    Transform cameraTransform;
    // Start is called before the first frame update
    void Start()
    {   
        playerTransform = player.transform;
        cameraTransform = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        cameraTransform.position = new UnityEngine.Vector3(playerTransform.position.x,playerTransform.position.y - 1 ,cameraTransform.position.z);     
    }
}
