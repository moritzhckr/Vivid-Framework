using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;

public class Spawner : MonoBehaviour
{

    public string spawnTimeStr;
    public Clock clock;
    private DateTime spawnTime;
    public CharacterSpawner characterSpawner;
    public Destinations destinations;
    int counter;
    public int spawnAmount =1;
    public bool spawnRandom;
    public bool spawnInterval;
    public bool spawnOnTime;
    public int repeatTime;

    public bool canSpawn = true;
    // Start is called before the first frame update
    void Start()
    {
        clock =  GameObject.Find("Scripts").GetComponent<Clock>();
        characterSpawner = GameObject.Find("Scripts").GetComponent<CharacterSpawner>();
        destinations = GameObject.Find("Scripts").GetComponent<Destinations>();
        InvokeRepeating("Count", 1, 1);

        spawnTimeStr = System.DateTime.Now.AddSeconds(2f).ToString();
        
          spawnTime = System.DateTime.Parse(spawnTimeStr);
        Debug.Log(spawnTime);
        Debug.Log(System.DateTime.Now);
    }

    // Update is called once per frame
    void Update()
    {
        //if (  System.DateTime.Compare(clock.time , _lastspawnTime) == 0)
        //{
        //    Spawn();
        //}
        if(spawnOnTime)
        {
            if (clock.time.Hour == spawnTime.Hour && clock.time.Minute == spawnTime.Minute && clock.time.Second == spawnTime.Second && canSpawn)
            {
                
                Spawn();
            }
        }
        
      

        if (spawnRandom)
        {
            int randomcount = UnityEngine.Random.Range(1, 10);
            if (counter == randomcount)
            {
                Debug.Log(randomcount);
                Spawn();
            }
            
        }
        else if(counter == repeatTime && spawnInterval)
        {
            Spawn();
        }

      
    }
 
    private void Spawn()
    {
        counter = 0;
         characterSpawner.SpawnGroup(spawnAmount, 1.0F, gameObject, destinations._destinations[UnityEngine.Random.Range(0, destinations._destinations.Length)]);
        canSpawn = false;
    }

    
   
    public void Count()
    {
        counter++;
    }
}
