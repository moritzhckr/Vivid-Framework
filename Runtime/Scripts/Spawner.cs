using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;

public class Spawner : MonoBehaviour
{
    private GameObject vividSpawnManager;
    private VividCharacterSpawner characterSpawner;
    private Destinations destinations;
    private float time = 0.0f;

    public int spawnAmount =1;
    public bool spawnInterval;
    public float repeatTime;

    public bool spawnRandom;
    public GameObject fixedDestination;


   
    public float randomMin;
    public float randomMax;


    // Start is called before the first frame update
    void Start()
    {
        //Get References to other Scripts
        vividSpawnManager = GameObject.FindObjectOfType<VividCharacterSpawner>().gameObject;
        characterSpawner = vividSpawnManager.GetComponent<VividCharacterSpawner>();
        destinations = vividSpawnManager.GetComponent<Destinations>();

        if (!spawnInterval)
        {
            Spawn();
        }
    }


    void Update()
    {
        
        //if(spawnOnTime)
        //{
        //    if (clock.time.Hour == spawnTime.Hour && clock.time.Minute == spawnTime.Minute && clock.time.Second == spawnTime.Second && canSpawn)
        //    {
                
        //        Spawn();
        //    }
        //}
        
      

        if (spawnInterval)
        {
            if (spawnRandom)
            {
                repeatTime = UnityEngine.Random.Range(randomMin,randomMax);
                time += Time.deltaTime;

                if (time >= repeatTime)
                {
                    time = time - repeatTime;

                    Spawn();
                }
            }
            else
            {
                time += Time.deltaTime;

                if (time >= repeatTime)
                {
                    time = time - repeatTime;

                    Spawn();
                }
            }
            
           
        }
        

      
    }
 
    private void Spawn()
    {
        if(fixedDestination == null)
        {
            characterSpawner.SpawnGroup(spawnAmount, 1.0F, gameObject, destinations._destinations[UnityEngine.Random.Range(0, destinations._destinations.Length)]);
            
        }
        else
        {
            characterSpawner.SpawnGroup(spawnAmount, 1.0F, gameObject, fixedDestination);
           
        }
       
    }

    
   
}
