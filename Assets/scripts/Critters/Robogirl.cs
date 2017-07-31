using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robogirl : Critter
{
    private void Start(){
         //GetComponent<MeshRenderer>().enabled = false;
         base.name = "Robogirl";
         base.points = 100;
    }
}
