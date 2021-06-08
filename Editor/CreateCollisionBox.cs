using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using System.Collections;

public class CreateCollisionBox : MonoBehaviour
{
   private  static int i;
    [MenuItem("Vivid/Create ColliderBox", false, 12)]
    
    static void Init()
    {
        //mymonoscript myScript = (mymonoscript)new mymonoscript();
        //myScript.createObject();
       
        GameObject instance = Instantiate(Resources.Load("AnimationCollider", typeof(GameObject))) as GameObject;
        string name = "AnimationCollider_" + i.ToString();
        instance.name = name;
       
        var tempMaterial = new Material(instance.GetComponent<MeshRenderer>().sharedMaterial);
        tempMaterial.color = new Color(Random.Range(0.1F,0.9F), Random.Range(0.1F, 0.9F), 0.4F, 0.5F);
        instance.GetComponent<MeshRenderer>().sharedMaterial = tempMaterial;


       
        // instance.GetComponent<MeshRenderer>().material.SetColor("_color", colliderCol );
        i++;
    }

  
}
