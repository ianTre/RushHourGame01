using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class NPCCar : MonoBehaviour
{
    public Node actualNode;
    private Node nextNode;
    bool ctrlHasBeingPressed = false;
    [SerializeField]
    Vector3 watch;
    private float velocity = 8; 
    private Vector3 totalDis;
    private float  totalTime; 
    float traveledDistance = 0;
    
    

    private void Update()
    {
        Node nextdest = GetNextNode();
        /*if(ctrlHasBeingPressed)
        {*/
            Debug.Log("I am about to move now");
            ctrlHasBeingPressed = false;
            

            if(traveledDistance < totalDis.magnitude)
            {
                Debug.Log("moving now");
                var delta = Time.deltaTime;
                var finalVector = delta * totalDis / totalTime;
                this.transform.Translate(finalVector);
                this.traveledDistance += finalVector.magnitude;
            }
            else
            {
                Debug.Log("i made it");
                this.actualNode = nextNode;
                nextNode = null;
            }
        /*
        }
        //TODO : REMOVE ctrlHasBeingPressed
        
        if(Input.GetKey(KeyCode.LeftControl))
        {
            Debug.Log("Control is being pressed");
            ctrlHasBeingPressed = true;
        }*/
    }

    private Node GetNextNode()
    {
        if(nextNode == null)
        {
            nextNode = actualNode.GetNextMove();
            Debug.Log("New Node is" + nextNode.name);
            CalculateConstants(nextNode);
        }
        return this.nextNode;
    }

    private void CalculateConstants(Node dest)
    {
        totalDis = dest.transform.position - this.transform.position;
        totalTime = totalDis.magnitude / velocity; 
        Debug.Log(totalDis.magnitude);
        this.traveledDistance = 0;
    }
}
