using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gateway : Critter
{
    private void Start(){
         //GetComponent<MeshRenderer>().enabled = false;
         base.name = "Gateway";
         base.points = 100;
    }
}
