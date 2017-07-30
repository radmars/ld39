using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : Critter
{
    private void Start(){
         //GetComponent<MeshRenderer>().enabled = false;
         base.name = "Fox";
         base.points = 100;
    }
}
