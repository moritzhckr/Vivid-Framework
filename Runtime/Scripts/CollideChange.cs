using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider))]
public class CollideChange : MonoBehaviour 
{


    private Collider _AreaColider;

    public Material ColMat;
    public Material NormalMat;

    public string[] AnimationName;
    bool isColliding = false;

   public  bool looping = false;

    public bool showAreaMesh = false;
    // Start is called before the first frame update
    void Start()
    {
       
            gameObject.GetComponent<MeshRenderer>().enabled = showAreaMesh;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
           
        }
    }

   
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 10)
        {
            collision.gameObject.GetComponent<Animator>().Play(RandomSelector(AnimationName));
        }
        Debug.Log("Collision");
        isColliding = true;
    }

    private void OnTriggerEnter(Collider other)
    {
       // other.gameObject.transform.GetComponentInChildren<SkinnedMeshRenderer>().material = ColMat;
        //other.gameObject.GetComponent<Animator>().SetBool(AnimationName, true);
       
        isColliding = true;

        StartCoroutine(FadeIn(other.gameObject.GetComponent<Animator>(), 1));
        StartCoroutine(StartAnimRandom(other));
    }

    private void OnTriggerExit(Collider other)
    {
        isColliding = false;
           Debug.Log("Trigger Exit");
        //other.gameObject.transform.GetComponentInChildren<SkinnedMeshRenderer>().material = NormalMat;
        //   other.gameObject.GetComponent<Animator>().SetLayerWeight(1, 0.01f);
        //other.gameObject.GetComponent<Animator>().SetBool(AnimationName, false);
        StopCoroutine(FadeIn(other.gameObject.GetComponent<Animator>(), 1));
        StartCoroutine(FadeOut(other.gameObject.GetComponent<Animator>(), 1));

    }
    private void OnCollisionExit(Collision collision)
    {
        
    }

    private string RandomSelector(string[] AnimationNames)
    {
        System.Random rnd = new System.Random();

        int randInt = rnd.Next(0, AnimationNames.Length);

        string RandomAnimName = AnimationNames[randInt].ToString();
        return RandomAnimName;
    }

    IEnumerator StartAnimRandom(Collider other)
    {
        //Print the time of when the function is first called.
        // Debug.Log("Started Coroutine at timestamp : " + Time.time);

       
        if (isColliding)
        {
            yield return new WaitForSeconds(Random.Range(0, 10));

            if (other.gameObject != null)
            {
                other.gameObject.GetComponent<Animator>().Play(RandomSelector(AnimationName));
            }
            else
            {
                yield break;
            }
                



            if (looping)
            {
                StartCoroutine(StartAnimRandom(other));
            }
        }
        else
        {
            StopCoroutine(StartAnimRandom(other));
        }
      


    }
    public IEnumerator FadeIn(Animator Animator, float time)
    {
        float weight = Animator.GetLayerWeight(1);
        while ( Animator != null && Animator.GetLayerWeight(1) < 1 && isColliding)
        {
            weight += Time.deltaTime / time;
            Animator.SetLayerWeight( 1, weight);
            yield return null;
        }
    }
    public IEnumerator FadeOut(Animator Animator, float time)
    {
        float weight = Animator.GetLayerWeight(1);
        while (Animator != null && Animator.GetLayerWeight(1) > 0)
        {
            weight -= Time.deltaTime / time;
            Animator.SetLayerWeight(1, weight);
            yield return null;
        }
       
    }

}
