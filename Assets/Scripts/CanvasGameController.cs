using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class CanvasGameController : MonoBehaviour
{
    [SerializeField] Text scoreText;
    Player player = null;
    public float priceToUpdate;
    public List<Hambuger> burgers;
    [SerializeField] GameObject iconsGameObject;
     private List<GameObject> burgerIcons;
     public List<Vector3> positions;
    void Start()
    {
        burgerIcons = new List<GameObject>();
        positions = new List<Vector3>();
        for (int i = 0; i < iconsGameObject.transform.childCount ; i++)
        {
            GameObject gO = iconsGameObject.transform.GetChild(i).gameObject;
            gO.GetComponent<RawImage>().color = new Color(0,0,0,0);
            Debug.Log("Adding new hamburger icon");
            burgerIcons.Add(gO);
            positions.Add(gO.transform.position);
        }
        burgers = new List<Hambuger>();
    }

    public void AddNewHamburger(Hambuger burger)
    {
        burger.beingTracked = true;
        burgers.Add(burger);
    }

    public void removeHamburger(Hambuger burger)
    {
        burgers.Remove(burger);
        GameObject icon = burger.GetIcon();
        Slider slider = icon.transform.Find("Slider").GetComponent<Slider>();
        slider.maxValue = 0;
        slider.value = 0;
        Transform fill = slider.transform.Find("Fill Area").transform.Find("Fill");
        fill.GetComponent<Image>().color = new Color(0,0,0,0);
    }

    void Update()
    {
        if (player == null)
        {
            player = FindObjectOfType<VehicleController>().player;
        }
        this.scoreText.text = this.scoreText.text.Replace("[Score]", player.Score().ToString());

        int index = 0;
        foreach (var burger in burgers)
        {
            
            burgerIcons[index].transform.position = positions[index]; //REVIEW THIS. IS NOT WORKING.
            burger.AssociateIcon(burgerIcons[index].gameObject);
            if (burger.visibleOnMap)
            {
                burgerIcons[index].GetComponent<RawImage>().color = burger.color;

            }

            if (burger.activeMission) //6000 : 5998 / 2
            {
                #region SLIDER
                Transform slider = burgerIcons[index].transform.Find("Slider");
                Transform arrow = burgerIcons[index].transform.Find("Arrow");


                if (!burger.hasBeingColored)
                {
                    Transform fill = slider.transform.Find("Fill Area").transform.Find("Fill");
                    fill.GetComponent<Image>().color = burger.color;
                    slider.GetComponent<Slider>().maxValue = burger.timeToEndMission;
                    burger.hasBeingColored = true;
                    arrow.GetComponent<Image>().color = burger.color; //SET THE COLOR FOR THE ARROW THAT POINTS WHERE THE PARKING LOT IS
                    arrow.GetComponent<ArrowDirectionController>().AssociateHamburgerToArrow(burger);
                }
                else
                {
                    slider.GetComponent<Slider>().value = burger.timeToEndMission;
                }

                if (burger.timeToEndMission == 0) //MISSION TIME OUT
                {
                    burger.beingTracked = false;
                }
                #endregion
            }
            index++;
            
        }
        
    }
}
