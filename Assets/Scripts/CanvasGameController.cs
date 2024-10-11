using System.Collections;
using System.Collections.Generic;
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
        burgers.Add(burger);
        Debug.Log("Adding new hamburger");
    }

    public void removeHamburger(Hambuger burger)
    {
        burgers.Remove(burger);
        GameObject icon = burger.GetIcon();
        Slider slider = icon.transform.Find("Slider").GetComponent<Slider>();
        slider.maxValue = 0;
        slider.value = 0;

        burger.DisAssociateIcon();
    }

    void Update()
    {
        if(player == null)
        {
            player = FindObjectOfType<VehicleController>().player;
        }   
        this.scoreText.text = this.scoreText.text.Replace("[Score]",player.Score().ToString());
        
        int index = 0;
        foreach (var burger in burgers)
        {
            burgerIcons[index].transform.position = positions[index]; //REVIEW THIS. IS NOT WORKING.
            burger.AssociateIcon(burgerIcons[index].gameObject);
            if(burger.visibleOnMap)
            {
                burgerIcons[index].GetComponent<RawImage>().color = burger.color;

            }
            if(burger.activeMission )
            {
                Transform slider = burgerIcons[index].transform.Find("Slider");

                
                if(!burger.hasBeingColored)
                {
                    Transform fill = slider.transform.Find("Fill Area").transform.Find("Fill");    
                    fill.GetComponent<Image>().color = burger.color;
                    slider.GetComponent<Slider>().maxValue = burger.timeToEndMission;
                    burger.hasBeingColored =true;
                }
                else
                {
                    slider.GetComponent<Slider>().value = burger.timeToEndMission;
                }
            }
            
            index++;
        }
        
    }
}
