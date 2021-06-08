using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using System.Collections;

public class CreateDestinationPoint : MonoBehaviour
{
   private  static int i;
    [MenuItem("Vivid/Create Destination Point", false, 12)]
    
    
    static void Init()
    {
      

        GameObject instance = Instantiate(Resources.Load("DestinationPoint", typeof(GameObject))) as GameObject;
        string name = "DestinationPoint_" + i.ToString();
        instance.name = name;
       instance.transform.position = new Vector3(0,0.5f,0);
        var tempMaterial = new Material(instance.GetComponent<MeshRenderer>().sharedMaterial);
        tempMaterial.color = new Color(0.81F, 0.02F, 0.03F);
        instance.GetComponent<MeshRenderer>().sharedMaterial = tempMaterial;
       
        
        i++;
    }

  
}
