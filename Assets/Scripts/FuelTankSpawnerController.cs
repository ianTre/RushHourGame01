using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FuelTankSpawnerController : MonoBehaviour
{
    // Start is called before the first frame update
    List<GameObject> fuelTanks;
    bool spawnHasBeingTriggered = false;
    void Start()
    {
        fuelTanks = new List<GameObject>();
        for (int i = 0; i < this.transform.childCount; i++)
        {
            GameObject fuelTank = this.transform.GetChild(i).gameObject;
            fuelTanks.Add(fuelTank);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if(!spawnHasBeingTriggered)
        {
            Invoke("ActivateFuelTank",5);
            spawnHasBeingTriggered = true;
        }
    }


    public void ActivateFuelTank()
    {
        var inactiveTanks = fuelTanks.Where(x => !x.activeInHierarchy).ToList();
        System.Random random  = new System.Random();
        int randomNumber = random.Next(0,inactiveTanks.Count - 1);
        if(randomNumber > 0)
            inactiveTanks[randomNumber].SetActive(true);
        spawnHasBeingTriggered = false;
    }
}
