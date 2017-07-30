using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skullboy : Critter
{
    private void Start(){
         //GetComponent<MeshRenderer>().enabled = false;
         base.name = "Skullboy";
         base.points = 100;
    }
}
