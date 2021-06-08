using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//using UnityStandardAssets.Characters.ThirdPerson;
using System;
public class CharacterSpawner : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Character_m;
    public GameObject Character_w;
    public List<GameObject> Avatars_m;
    public Destinations _destinations;
    public GameObject SpawnPoint;

    //Number of Spawend Characters
    private int count = 0;
    [Range(0f, 100f)][SerializeField] float percentFemalesInGroup = 50.0f;
    private List<GameObject> SceneCharacters;
    List<Dictionary<string, object>> namesCSVList;
    private void Awake()
    {
        List<Dictionary<string, object>> namesCSV = CSVReader.Read("VornamenNachnamen");
        namesCSVList = namesCSV;
        _destinations = gameObject.GetComponent<Destinations>();
        
       
    }
    void Start()
    {
        SceneCharacters = new List<GameObject>();
      //  SpawnCharacters();
       

    }

  public void  setCharacterMesh(){

    }
    void setDestination(GameObject character, GameObject destination)
    {
        character.GetComponent<CharacterStats>().target = destination;
       
        character.GetComponent<MoveTarget>().destroyOnDestination = destination.gameObject.GetComponent<DestinationPoint>().destroyOnArrival;
       
    }


    void SpawnCharacter(GameObject character,GameObject startPoint, GameObject target, bool isFemale)
    {
       
        GameObject newCharacter = character;
        string characterName = "";
        characterName = "NavMeshAgent "+ count.ToString(); ;
        if (isFemale == true)
        {
            
            newCharacter.GetComponent<CharacterStats>().isFemale = true;
            newCharacter.GetComponent<CharacterStats>().characterName = preName(true);
        }
        else
        {
          
            newCharacter.GetComponent<CharacterStats>().isFemale = false;
            newCharacter.GetComponent<CharacterStats>().characterName = preName(false);
        }
        setDestination(newCharacter, target);

        GameObject.Instantiate(character, startPoint.transform.position, Quaternion.identity);
        
        newCharacter.GetComponent<NavMeshAgent>().speed = UnityEngine.Random.Range(0.47f,0.57f);
        newCharacter.GetComponent<NavMeshAgent>().radius = UnityEngine.Random.Range(0.47f, 0.57f);
        newCharacter.GetComponent<CharacterStats>().age = generateAge();
        newCharacter.GetComponent<CharacterStats>().lastName = generateLastname();
        //newCharacter.name = newCharacter.name +"_" + count ;
        newCharacter.transform.name = characterName ;

        SceneCharacters.Add(newCharacter);
        Debug.Log("Instatiated: " + newCharacter.GetComponent<CharacterStats>().characterName + " " + newCharacter.GetComponent<CharacterStats>().lastName + " " + newCharacter.GetComponent<CharacterStats>().age);

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
                SpawnCharacter(Character_w, SpawnPoint, target, isFemale);
            }
            else
            {
                SpawnCharacter(Avatars_m[UnityEngine.Random.Range(0, Avatars_m.Count)],SpawnPoint, target, isFemale);
            }
            
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
          //  Transform target = _destinations._destinations[Random.Range(0, _destinations._destinations.Length)].transform;

            GameObject target = _destinations._destinations[UnityEngine.Random.Range(0, _destinations._destinations.Length)];
    
          //   SpawnGroup(10,percentFemalesInGroup, target);

           SpawnGroup(10, percentFemalesInGroup,SpawnPoint, target);

        }

    }
    public void SpawnGroup(int count, float percentfemales,GameObject startPoint, GameObject target){

        //Debug.Log("COUNT : "+ count +"percentfemales FEMALES: "+ percentfemales);
      float females = (percentfemales /100 )* count;
     
        int roundedUp = (int)Math.Ceiling(females);
       // Debug.Log( females +"COUNT FEMALES roundedUp: "+ roundedUp );

    for (int i = 0; i < roundedUp; i++)
                {

                   
                        SpawnCharacter(Character_w, startPoint, target, true);
                  
                }
                for (int i = 0; i < count-roundedUp; i++)
                {

                   
                        SpawnCharacter(Avatars_m[UnityEngine.Random.Range(0, Avatars_m.Count)], startPoint, target, false);
                    
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
