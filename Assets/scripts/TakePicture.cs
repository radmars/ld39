 using UnityEngine;
using System.Collections;
 using System.Collections.Generic;
 public class TakePicture: MonoBehaviour
 {
    private GameObject[] Critters;
    private GameObject[] VisibleCritters;
 void Start () {
         Critters = GameObject.FindGameObjectsWithTag("Critter");
         VisibleCritters = new GameObject[0];
     }
     
     // Update is called once per frame
     void Update () {
        if(!Input.GetMouseButtonDown(0) || !Input.GetButtonDown("Fire1"))
            return;

            Debug.Log("Click");
             for(int i = 0 ; i < Critters.Length ; i++) //See what critters are visible
             {
                 if(Critters[i].GetComponent<MeshRenderer>().isVisible == true) {
                     //Visiblecritters[i] = critters[i];
                     System.Collections.Generic.List<GameObject> list = new System.Collections.Generic.List<GameObject>(Critters);
                     list.Remove(Critters[i]);
                     VisibleCritters = list.ToArray();
                 }
             }
             
             if(VisibleCritters.Length > 0) { //If there are no visible critters
                foreach(var critter in Critters)
                {
                    Debug.Log(critter.tag);
                }
             }
     }
 }