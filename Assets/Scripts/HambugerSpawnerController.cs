using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class HambugerSpawnerController : MonoBehaviour
{
    int hambugerSpotNumber = 13;
    bool needToSpawn = false;
    // Start is called before the first frame update
    void Start()
    {
        System.Random random  = new System.Random();
        int randomNumber = random.Next(0,hambugerSpotNumber - 1);
        SpotHambuger(randomNumber);
        Invoke("changeFlag",10);
    }

    // Update is called once per frame
    void Update()
    {
        if(needToSpawn)
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
        hambugerSpotNumber--;
    }
}
