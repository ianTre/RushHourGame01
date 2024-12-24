using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class ArrowDirectionController : MonoBehaviour
{
    public Hambuger hambuger;
    private bool isVisibleInCanvas = false;
    private Transform player;
    public float fixedAngle = 0;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<VehicleController>().transform;
    }

    public void AssociateHamburgerToArrow(Hambuger hambuger)
    {
        Debug.Log("arrow is being associated now");
        this.hambuger = hambuger;
        this.isVisibleInCanvas = true;
    }


    // Update is called once per frame
    void Update()
    {
        if(isVisibleInCanvas)
        {
            if(hambuger == null)
            {
                resetArrow();
                return;
            }
            Debug.Log("arrow is visible now");
            float angle = Rotate(player,hambuger.getParkingLot());
            Debug.Log("Angle is :" + angle.ToString());
            //transform.Rotate(0,0,angle);       
            transform.eulerAngles = new Vector3(0,0,angle);
        }

    }

    private void resetArrow()
    {   
        this.GetComponent<Image>().color = new Color(0,0,0,0);
        isVisibleInCanvas = false;
    }

    private float Rotate(Transform player,Transform targetToLook)
    {
        //Vector3 TargetLook = player.InverseTransformPoint(targetToLook.position);
        float angle = Mathf.Atan2(player.position.x - targetToLook.position.x , targetToLook.position.y - player.position.y) * Mathf.Rad2Deg + fixedAngle  ;
        return angle;
    }
}
