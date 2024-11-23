using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class NPCCar : MonoBehaviour
{
    public Node actualNode;
    public Node TargetNode;
    bool ctrlHasBeingPressed = false;
    [SerializeField]
    Vector3 watch;
    [SerializeField]
    Quaternion quaternion;
    public float NPCSpeed = 10;
    
    private void Update()
    {
        GetNextNode();
        
            ctrlHasBeingPressed = false;
            transform.position = Vector3.MoveTowards(transform.position , TargetNode.transform.position , NPCSpeed * Time.deltaTime);
            Rotate();
    }

    private void Rotate()
    {
        Vector3 TargetLook = transform.InverseTransformPoint(TargetNode.transform.position);
        watch = TargetLook;
        float angle = Mathf.Atan2(TargetLook.y,TargetLook.x) * Mathf.Rad2Deg - 90 ;
        transform.Rotate(0,0,angle);   
    }

    private Node GetNextNode()
    {
        System.Random random  = new System.Random();
        int randomNumber = random.Next(0,2);
        Debug.Log(randomNumber);


        if(TargetNode == null)
        {
            TargetNode = actualNode.GetNextMove();
        }
        if(transform.position == TargetNode.transform.position)
        {
            actualNode = TargetNode;
            TargetNode = actualNode.GetNextMove();
        }
        return this.TargetNode;
    }
}
