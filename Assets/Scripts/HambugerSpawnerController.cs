using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class HambugerSpawnerController : MonoBehaviour
{
    int hambugerSpotNumber = 13;
    bool needToSpawn = false;
    [SerializeField] Color[] colors;
    private List<Color> avaiableColors;
    [SerializeField]int activeMissionsMax = 1;
    public int VisibleMissionsOnMap = 0;
    public int activeMissions = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        avaiableColors = new List<Color>();
        foreach(Color color in colors)
        {
            avaiableColors.Add(color);
        }
        Invoke("changeFlag",10);
    }

    // Update is called once per frame
    void Update()
    {
        if(needToSpawn && VisibleMissionsOnMap < activeMissionsMax)
        {
            System.Random random  = new System.Random();
            int randomNumber = random.Next(0,hambugerSpotNumber - 1);
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
        Transform transform= this.gameObject.transform.GetChild(index);
        transform.gameObject.SetActive(true);
        transform.gameObject.GetComponent<Hambuger>().color = GetColor();
        hambugerSpotNumber--;
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
