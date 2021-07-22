using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(NavMeshAgent))]
public class MoveCharacter : MonoBehaviour
{
    public NavMeshAgent nmAgent;


    public Vivid_ThirdPersonCharacter thirdPersonCharacter;
    public bool destroyOnDestination = false;
  
    private void Start()
    {
        nmAgent = gameObject.GetComponent<NavMeshAgent>();
        nmAgent.updateRotation = false;
        thirdPersonCharacter = nmAgent.GetComponent<Vivid_ThirdPersonCharacter>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))//If the player has left clicked
        {
            Vector3 mouse = Input.mousePosition;//Get the mouse Position
            Ray castPoint = Camera.main.ScreenPointToRay(mouse);//Cast a ray to get where the mouse is pointing at
            RaycastHit hit;//Stores the position where the ray hit.
            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))//If the raycast doesnt hit a wall
            {
                nmAgent.SetDestination(hit.point);
                nmAgent.gameObject.GetComponent<Collider>().enabled = true;
            }

           
           
        }
        if (nmAgent.remainingDistance > nmAgent.stoppingDistance)
        {
            thirdPersonCharacter.Move(nmAgent.desiredVelocity, false, false);
            
        }
        else
        {
            thirdPersonCharacter.Move(Vector3.zero, false, false);

           
           
        }
        if (nmAgent.remainingDistance > nmAgent.stoppingDistance)
        {
            thirdPersonCharacter.Move(nmAgent.desiredVelocity, false, false);
            if (nmAgent.remainingDistance < nmAgent.stoppingDistance + 1)
            {
                if (destroyOnDestination)
                {
                    Debug.Log("got Distroy call");
                    Destroy(gameObject);
                }

            }
        }
        else
        {
            thirdPersonCharacter.Move(Vector3.zero, false, false);



        }
    }
}