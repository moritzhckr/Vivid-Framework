using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.AI;
public class AnimationObject : MonoBehaviour
{

    public Transform _destinationTransform;
    public GameObject occupant;
    public AnimationClip _Animclip;
    public AnimationClip _Animclip_standup;
    public GameObject seatPosition;
    VividCharacterSpawner vividCharacterSpawner;
    Animator m_Animator;

    public float sittingTime;

    private void Start()
    {
        if (vividCharacterSpawner == null)
        {
            vividCharacterSpawner = GameObject.FindObjectOfType<VividCharacterSpawner>();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
         if(other.gameObject.GetComponent<VividCharacter>().target == _destinationTransform.gameObject)
        {
            occupant = other.gameObject;
            other.gameObject.GetComponent<Collider>().enabled = false;
            m_Animator = other.gameObject.GetComponent<Animator>();
            m_Animator.SetLayerWeight(2, 1.0f);
            m_Animator.SetLayerWeight(1, 0.01f);
            m_Animator.SetLayerWeight(0, 0.01f);
            m_Animator.Play(_Animclip.name);
           
           other.gameObject.transform.rotation = this.transform.rotation;
            StartCoroutine(TimeToStandup(sittingTime));
        }
       
    }
    private void LateUpdate()
    {
        if (occupant != null  )
        {
            occupant.gameObject.transform.rotation = this.transform.rotation;
            StartCoroutine(LerpPosition(seatPosition.transform.position, 0.5f));
            m_Animator.SetBoneLocalRotation(HumanBodyBones.Hips, gameObject.transform.rotation);
        }
        
    }
    
    IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = occupant.gameObject.transform.position;

        while (time < duration && occupant != null)
        {
            occupant.gameObject.transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        if (occupant != null)
        {
            occupant.gameObject.transform.position = targetPosition;
        }
            
      
    }
    IEnumerator TimeToStandup( float duration)
    {
        float time = 0;
        

        while (time < duration)
        {
            time += Time.deltaTime;
            
            if(time >= duration)
            {
                StandUp();
            }
            yield return null;
        }
       

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            StandUp();
        }

    }
    private void StandUp()
    {
        m_Animator.Play(_Animclip_standup.name);
        m_Animator.SetLayerWeight(0, 1.0f);
        m_Animator.SetLayerWeight(2, 0.0f);

        GameObject newDestination = vividCharacterSpawner._destinations._destinations[0];
        occupant.GetComponent<NavMeshAgent>().SetDestination(newDestination.transform.position);
        occupant.GetComponent<VividCharacter>().target = newDestination;
        occupant.gameObject.GetComponent<Collider>().enabled = true;
        occupant = null;
    }
    
}
