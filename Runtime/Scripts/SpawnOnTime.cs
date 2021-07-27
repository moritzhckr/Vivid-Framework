using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using SimpleJSON;



// look up JsonUtility in the Unity api if this bit confuses you
[Serializable]
public class SpawnInfo
{
   public GameObject startPoint;
    public int characterCount;
    public float percentFemales;
    public GameObject destinationPoint;
    public int TimeHour;
    public int TimeMinute;
    public int TimeSeconds;
}



public class SpawnOnTime : MonoBehaviour
{
    public VividCharacterSpawner vividCharacterSpawner;
    public Clock clock;
    public bool DebugModeOn = true;
    public string jsonFileName = "spawnPlan.json";
    public List<SpawnInfo> spawnInfos;

    void Start()
    {
        if(vividCharacterSpawner == null)
        {
            vividCharacterSpawner = GameObject.FindObjectOfType<VividCharacterSpawner>();
        }
        if (!String.IsNullOrEmpty(jsonFileName))
        {
            ParseJsonPlan(LoadResourceTextfile(jsonFileName));

        }


    }

    private void ParseJsonPlan(string jsonString)
    {
        var N = JSON.Parse(jsonString);
        var   arr = N["SpawnList"].AsArray;
        if (DebugModeOn)
        {
            Debug.Log("Found " + arr.Count + "SpawnInfo Items in the json file");
        }
       
        for (int i = 0; i < arr.Count; i++)
        {
            SpawnInfo tempSpawn = new SpawnInfo();
            tempSpawn.startPoint = GameObject.Find(arr[i]["startPoint"]);
            tempSpawn.characterCount = arr[i]["characterCount"].AsInt;
          
            tempSpawn.destinationPoint = GameObject.Find(arr[i]["destinationPoint"]);
            tempSpawn.TimeHour = arr[i]["TimeHour"].AsInt;
            tempSpawn.TimeMinute = arr[i]["TimeMinute"].AsInt;
            tempSpawn.TimeSeconds = arr[i]["TimeSeconds"].AsInt;
            tempSpawn.percentFemales = arr[i]["percentFemales"].AsFloat;
            if (DebugModeOn)
            {
                Debug.Log(tempSpawn.percentFemales + "  " + arr[i]["percentFemales"].AsInt + "  " + arr[i]["percentFemales"].AsFloat);
            } 
           
            spawnInfos.Add(tempSpawn);
           
        }
     


    }
    public static string LoadResourceTextfile(string path)
    {
        string filePath =  path.Replace(".json", "");
        TextAsset targetFile = Resources.Load<TextAsset>(filePath);
        Debug.Log("Loaded: " + path + "  " + targetFile.text);
        return targetFile.text;
    }

    bool blockdoubleCalls = true;
    private IEnumerator PlanedSpawn(int count, float percent, GameObject Start, GameObject Destination)
    {

        yield return new WaitForSecondsRealtime(1);
        if (DebugModeOn)
        {
            Debug.Log("Spawned: " + count + " Characters " + percent + "% are Females- Starting from: " + Start.name + "to " + Destination.name);
        }
       
        blockdoubleCalls = true;
        vividCharacterSpawner.SpawnGroup(count, percent, Start, Destination);
      //  vividCharacterSpawner.SpawnGroup(count, percent, Start, Destination);
       
    }

    private void Update()
    {
       
        foreach (var item in spawnInfos)
        {
            if (clock.time.Hour == item.TimeHour && blockdoubleCalls)
            {
                if (clock.time.Minute == item.TimeMinute && blockdoubleCalls)
                {
                    if (clock.time.Second == item.TimeSeconds && blockdoubleCalls)
                    {
                        blockdoubleCalls = false;
                      StartCoroutine( PlanedSpawn(item.characterCount, item.percentFemales, item.startPoint, item.destinationPoint));
                        if (DebugModeOn)
                        {
                            Debug.Log("SpawnTime: " + item.TimeHour + ": " + item.TimeMinute + ": " + item.TimeSeconds );
                        }

                    }

                }

            }
        }
    }
}
