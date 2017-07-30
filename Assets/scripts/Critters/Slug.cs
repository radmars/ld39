using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slug : Critter
{
    private void Start(){
         //GetComponent<MeshRenderer>().enabled = false;
         base.name = "Slug";
         base.points = 100;
    }
}
