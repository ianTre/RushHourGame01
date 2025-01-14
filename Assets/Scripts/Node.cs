using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Node : MonoBehaviour
{
    public Node Origin;
    public List<Node> Connections;
    public int Id;

    void Start()
    {
        this.transform.rotation = Quaternion.identity;
    }


    
    public Node GetNextMove()
    {
        if(Connections.Count == 0)
            return this;

        /*Pick Random Option on each Intersection*/
        List<Node> possibleMovements = Connections.ToList();
        int optionsNumber = possibleMovements.Count;
        System.Random random  = new System.Random();
        int randomNumber = random.Next(0,optionsNumber); //NOTE : Next is always lesser than max number. So Next(0,1) will always be 0. 
        return possibleMovements[randomNumber];
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        if(Connections.Count > 0)
        {
            for (int i = 0; i < Connections.Count; i++)
            {
                Gizmos.DrawLine(transform.position , Connections[i].transform.position);
            }
        }
    }
}
