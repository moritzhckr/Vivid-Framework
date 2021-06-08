using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using System.Collections;

public class CreateStartPoint : MonoBehaviour
{
   private  static int i;
    [MenuItem("Vivid/Create Start Point", false, 12)]
    
    
    static void Init()
    {
      

        GameObject instance = Instantiate(Resources.Load("StartPoint", typeof(GameObject))) as GameObject;
        string name = "StartPoint_" + i.ToString();
        instance.name = name;
       instance.transform.position = new Vector3(0,0.5f,0);
        var tempMaterial = new Material(instance.GetComponent<MeshRenderer>().sharedMaterial);
        tempMaterial.color = new Color(0.046F, 0.66F, 0.29F);
        instance.GetComponent<MeshRenderer>().sharedMaterial = tempMaterial;
       
        
        i++;
    }

  
}
