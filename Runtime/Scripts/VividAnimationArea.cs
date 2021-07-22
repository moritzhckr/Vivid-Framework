using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider))]
public class VividAnimationArea : MonoBehaviour 
{
       
    public string[] AnimationNames;
    bool isColliding = false;

   public  bool looping = false;
    public int animationLayer;
    public bool showAreaMesh = false;
    // Start is called before the first frame update
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
    private void OnTriggerStay(Collider other)
    {
        
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
        string animationNames = RandomSelector(AnimationNames);

        if (isColliding)
        {
            Debug.Log("isColliding");
            yield return new WaitForSeconds(Random.Range(2, 10));

            if (other.gameObject != null)
            {
                Debug.Log("other.gameObject != null "+ animationNames);
                other.gameObject.GetComponent<Animator>().Play(animationNames);
            }
            else
            {
                yield break;
            }

            if (looping && AnimatorIsPlaying(other.gameObject.GetComponent<Animator>()))
            {
                Debug.Log("looping");
                StartCoroutine(StartAnimRandom(other));
            }
        }
        else
        {
            Debug.Log("StopCoroutine");
          //  StopCoroutine(StartAnimRandom(other));
            
        }
      


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
