using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Node : MonoBehaviour
{
    public Node Origin;
    public List<Node> Connections;

    public Node GetNextMove()
    {
        if(Connections.Count == 0)
            return this;

        List<Node> possibleMovements = Connections.Where(x => x != Origin).ToList();
        //TODO : If there are more than one option , choose with random.

        //TODO

        return possibleMovements[0];
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
