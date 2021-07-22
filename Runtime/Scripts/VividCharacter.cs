using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//using UnityStandardAssets.Characters.ThirdPerson;


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(MoveCharacter))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Vivid_ThirdPersonCharacter))]

public class VividCharacter : MonoBehaviour
{
    public string characterName;
    public string lastName;
    public int age;
    public string occupation = "Student";
    public bool isFemale;
    private Destinations _destinations;
    public GameObject target;
     public List<GameObject> characterMeshsFemale;
     public List<GameObject> characterMeshsMale;
    private void Awake()
    {

        //_destinations = GameObject.Find("Scripts").GetComponent<Destinations>();
        if(target != null)
        {
            //set Destinations
            gameObject.GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
        }
       

   

if( isFemale)
{
  characterMeshsFemale[ UnityEngine.Random.Range(0,characterMeshsFemale.Count)].SetActive(true);
}else{
    characterMeshsMale[ UnityEngine.Random.Range(0,characterMeshsMale.Count)].SetActive(true);
}

    }





}
