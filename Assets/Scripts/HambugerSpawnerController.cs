using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class HambugerSpawnerController : MonoBehaviour
{
    bool needToSpawn = false;
    [SerializeField] Color[] colors;
    private List<Color> avaiableColors;
    [SerializeField]int activeMissionsMax = 1;
    public int VisibleMissionsOnMap = 0;
    public int activeMissions = 0;
    private CanvasGameController canvasController;
    private List<Transform> listOfHamburgerToStop;
    
    // Start is called before the first frame update
    void Start()
    {
        avaiableColors = new List<Color>();
        setupListOfHamburgers();
        canvasController = FindObjectOfType<CanvasGameController>();
        foreach(Color color in colors)
        {
            avaiableColors.Add(color);
        }
        Invoke("changeFlag",10);

    }

    private void setupListOfHamburgers()
    {
        listOfHamburgerToStop = new List<Transform>();
        for (int i = 0; i < this.gameObject.transform.childCount; i++)
        {
            listOfHamburgerToStop.Add(this.gameObject.transform.GetChild(i));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(needToSpawn && VisibleMissionsOnMap < activeMissionsMax)
        {
            System.Random random  = new System.Random();
            int randomNumber = random.Next(0,listOfHamburgerToStop.Count - 1);
            SpotHambuger(randomNumber);
            Invoke("changeFlag",10);
        }
    }

    void changeFlag()
    {
        this.needToSpawn = true;
    }

    void SpotHambuger(int index)
    {
        needToSpawn = false;
        Transform transform= listOfHamburgerToStop[index];
        listOfHamburgerToStop.Remove(transform);
        Hambuger hamburger;
        transform.gameObject.SetActive(true);
        hamburger = transform.gameObject.GetComponent<Hambuger>();
        hamburger.color = GetColor();
        hamburger.visibleOnMap = true;
        hamburger.timeToPickMission = 240;
        canvasController.AddNewHamburger(hamburger);
        VisibleMissionsOnMap++;
    }

    public Color GetColor()
    {
        if(!avaiableColors.Any())
            throw new Exception("You are trying to activate more missions than the avaiable colors. Check VisibleMissionsOnMap , activeMissionsMax or avaiableColors");
        Color color = avaiableColors.First();
        avaiableColors.Remove(color);
        return color;
    }

    public void returnColor(Color color)
    {
        avaiableColors.Add(color);
        VisibleMissionsOnMap--;
        activeMissions--;
    }
}
