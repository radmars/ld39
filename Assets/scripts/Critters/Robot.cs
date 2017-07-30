using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : Critter
{
    private void Start(){
         //GetComponent<MeshRenderer>().enabled = false;
         base.name = "Robot";
         base.points = 100;
    }
}
