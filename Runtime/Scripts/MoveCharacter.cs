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
                DestinationPoint destination = gameObject.GetComponent<VividCharacter>().target.gameObject.GetComponent<DestinationPoint>();
                AnimationObject animationObject = gameObject.GetComponent<VividCharacter>().target.gameObject.GetComponent<AnimationObject>();
                if (animationObject != null && destination == null)
                {
                    Debug.Log("has AnimationObject");
                    nmAgent.stoppingDistance = 0.0f;
                }
                if (destination != null)
                {
                    Debug.Log("has destination");
                    if (gameObject.GetComponent<VividCharacter>().target.gameObject.GetComponent<DestinationPoint>().destroyOnArrival)
                    {

                        Destroy(gameObject);
                    }
                }
                

            }
        }
        else
        {
            thirdPersonCharacter.Move(Vector3.zero, false, false);



        }
    }
}