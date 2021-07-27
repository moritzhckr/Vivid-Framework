using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider))]
public class VividAnimationArea : MonoBehaviour 
{
    public string[] AnimationNames;
    public int animationLayer;
    public bool showAreaMesh = false;
    bool isColliding = false;
    void Start()
    {
       
            gameObject.GetComponent<MeshRenderer>().enabled = showAreaMesh;
        
    }
 

    private void OnTriggerEnter(Collider other)
    {
        
        isColliding = true;
        StartCoroutine(FadeIn(other.gameObject.GetComponent<Animator>(), 1));
        StartCoroutine(StartAnimRandom(other));
    }

    private void OnTriggerExit(Collider other)
    {
        isColliding = false;
        StopCoroutine(FadeIn(other.gameObject.GetComponent<Animator>(), 1));
        StartCoroutine(FadeOut(other.gameObject.GetComponent<Animator>(), 1));

    }
  

    private string RandomSelector(string[] AnimationNames)
    {
        System.Random rnd = new System.Random();

        int randInt = rnd.Next(0, AnimationNames.Length);
      
        string RandomAnimName = AnimationNames[randInt].ToString();
        return RandomAnimName;
    }
    bool AnimatorIsPlaying(Animator animator)
    {
        Debug.Log("oAnimatorIsPlaying " + animator.GetCurrentAnimatorStateInfo(0).length);
        return animator.GetCurrentAnimatorStateInfo(0).length >
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }
    IEnumerator StartAnimRandom(Collider other)
    {
        Animator m_Animator;
        string m_ClipName;
        AnimatorClipInfo[] m_CurrentClipInfo;

        float m_CurrentClipLength;
        string animationName = RandomSelector(AnimationNames);

        if (isColliding)
        {
            Debug.Log("isColliding");
            yield return new WaitForSeconds(Random.Range(2, 10));

            if (other.gameObject != null)
            {
                Debug.Log("other.gameObject != null "+ animationName);
                other.gameObject.GetComponent<Animator>().Play(animationName);
                
            }
               
            else
            {
                yield break;
            }

            
        }
        else
        {
            Debug.Log("StopCoroutine");
          //  StopCoroutine(StartAnimRandom(other));
            
        }
      


    }
    public AnimationClip FindAnimation(Animator animator, string name)
    {
        foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == name)
            {
                return clip;
            }
        }

        return null;
    }

    public IEnumerator FadeIn(Animator Animator, float time)
    {
        float weight = Animator.GetLayerWeight(animationLayer);
        while ( Animator != null && Animator.GetLayerWeight(animationLayer) < 1 && isColliding)
        {
            weight += Time.deltaTime / time;
            Animator.SetLayerWeight(animationLayer, weight);
            yield return null;
        }
    }
    public IEnumerator FadeOut(Animator Animator, float time)
    {
        Debug.Log("FadeOut");
        float weight = Animator.GetLayerWeight(animationLayer);
        while (Animator != null && Animator.GetLayerWeight(animationLayer) > 0)
        {
            weight -= Time.deltaTime / time;
            Animator.SetLayerWeight(animationLayer, weight);
            yield return null;
        }
       
    }

}
