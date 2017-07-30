using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : Critter
{
    private void Start(){
         //GetComponent<MeshRenderer>().enabled = false;
         base.name = "Wolf";
         base.points = 100;
    }
}
