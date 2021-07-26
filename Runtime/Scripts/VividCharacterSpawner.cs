using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//using UnityStandardAssets.Characters.ThirdPerson;
using System;
public class VividCharacterSpawner : MonoBehaviour
{
    // Start is called before the first frame update

    //public GameObject Character_m;
    //public GameObject Character_w;
    public List<GameObject> avatars_m;
    public List<GameObject> avatars_f;
    public Destinations _destinations;
    public GameObject SpawnPoint;
    public int spawnGroupCount;

    //Number of Spawend Characters
    private int count = 0;
    [Range(0f, 100f)][SerializeField] float percentFemalesInGroup = 50.0f;
    List<Dictionary<string, object>> namesCSVList;

    public bool DebugModeOn = false;

    private void Awake()
    {
        List<Dictionary<string, object>> namesCSV = CSVReader.Read("VornamenNachnamen");
        namesCSVList = namesCSV;
        _destinations = gameObject.GetComponent<Destinations>();
        count = 0;

    }


   

    void SpawnCharacter(GameObject character,GameObject startPoint, GameObject target, bool isFemale)
    {
        GameObject newCharacter = character;

        Debug.Log(character.name);
        newCharacter = Instantiate(character, startPoint.transform.position, Quaternion.identity);
       
        if (isFemale == true)
        {

            newCharacter.GetComponent<VividCharacter>().isFemale = true;
            newCharacter.GetComponent<VividCharacter>().characterName = preName(true);
           
        }
        else
        {

            newCharacter.GetComponent<VividCharacter>().isFemale = false;
            newCharacter.GetComponent<VividCharacter>().characterName = preName(false);
        }
        newCharacter.GetComponent<VividCharacter>().target = target;
        newCharacter.GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
        // newCharacter = Instantiate<GameObject>(character, startPoint.transform.position, Quaternion.identity);
        // newCharacter = Instantiate(character, startPoint.transform.position, Quaternion.identity);
        string characterName = "NavMeshAgent " + count.ToString(); ;
         newCharacter.name = characterName;

        newCharacter.GetComponent<NavMeshAgent>().speed = UnityEngine.Random.Range(0.40f, 0.60f);
        newCharacter.GetComponent<NavMeshAgent>().radius = UnityEngine.Random.Range(0.47f, 0.57f);
        newCharacter.GetComponent<VividCharacter>().age = generateAge();
        newCharacter.GetComponent<VividCharacter>().lastName = generateLastname();

        if (DebugModeOn)
        {
            Debug.Log("Instatiated: " + newCharacter.GetComponent<VividCharacter>().characterName + " " + newCharacter.GetComponent<VividCharacter>().lastName + " " + newCharacter.GetComponent<VividCharacter>().age);
        }


        count++;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject target = _destinations._destinations[UnityEngine.Random.Range(0, _destinations._destinations.Length)];
            bool isFemale = randomBool();
            if (isFemale == true)
            {
                SpawnCharacter(avatars_f[UnityEngine.Random.Range(0, avatars_f.Count)], SpawnPoint, target, isFemale);
            }
            else
            {
                SpawnCharacter(avatars_m[UnityEngine.Random.Range(0, avatars_m.Count)],SpawnPoint, target, isFemale);
            }
            
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
          GameObject target = _destinations._destinations[UnityEngine.Random.Range(0, _destinations._destinations.Length)];
          SpawnGroup(spawnGroupCount, percentFemalesInGroup,SpawnPoint, target);

        }

    }
    public void SpawnGroup(int count, float percentfemales,GameObject startPoint, GameObject target){

        int w = (count * Mathf.RoundToInt(percentfemales)) / 100;
        int m = count - w;
        Debug.Log("Spawn:" + w + " Females, " + m + " Males");
        for (int i = 0; i < w; i++)
                {
          
            SpawnCharacter(avatars_f[UnityEngine.Random.Range(0, avatars_f.Count)], startPoint, target, true);             
                }
        for (int j = 0; j < m; j++)
                {
          
            SpawnCharacter(avatars_m[UnityEngine.Random.Range(0, avatars_m.Count)], startPoint, target, false);   
                }
    }

    

    string generateLastname()
    {
        string returnLasteName  = namesCSVList[UnityEngine.Random.Range(1, 49)]["lastname"].ToString(); ;
       
        return returnLasteName;
    }
        
    string preName(bool isFemale)
    {
        string returnName = "";
        if (isFemale)
        {
            returnName =  namesCSVList[UnityEngine.Random.Range(1, 50)]["name_w"].ToString();
        }
        else
        {
            returnName = namesCSVList[UnityEngine.Random.Range(1, 50)]["name_m"].ToString();
        }

        return returnName;
    }
    int generateAge()
    {
        int returnAge = UnityEngine.Random.Range(18, 35);

        return returnAge;
    }
       
    bool randomBool()
    {
        bool returnbool = false;
        System.Random rnd = new System.Random();
        if (rnd.Next(2) == 0)
            returnbool = true;
        


        return returnbool;
    }
    
}
