using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using System.Collections;

public class CreateAnimationArea : MonoBehaviour
{
   private  static int i;
    [MenuItem("Vivid/Create AnimationArea", false, 12)]
    
    static void Init()
    {
       
        GameObject instance = Instantiate(Resources.Load("AnimationArea", typeof(GameObject))) as GameObject;
        string name = "AnimationArea_" + i.ToString();
        instance.name = name;
       
        var tempMaterial = new Material(instance.GetComponent<MeshRenderer>().sharedMaterial);
        tempMaterial.color = new Color(Random.Range(0.1F,0.9F), Random.Range(0.1F, 0.9F), 0.4F, 0.5F);
        instance.GetComponent<MeshRenderer>().sharedMaterial = tempMaterial;
        i++;
    }

  
}
