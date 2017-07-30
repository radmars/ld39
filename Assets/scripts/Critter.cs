using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Critter : MonoBehaviour
{
    public string name;
    public double points;

    public double DistanceModifier = .02;

    public double FacingModifier = 5;
    private void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
        gameObject.tag = "Critter";
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5F);
        Gizmos.DrawCube(transform.position, new Vector3(1, 1, 1));
    }

    public void ActivateCritter()
    {
        GetComponent<MeshRenderer>().enabled = true;
    }

    public double CalculatePoints(GameObject player)
    {
        var picValue = points;

        //Distance from player
        var distance = Vector3.Distance(transform.position, player.transform.position);
       // Debug.Log("the distance is :" + distance);
        if (distance < 25)
        {
            picValue = picValue * (DistanceModifier * (100 - distance));
        }

        //how close they are to facing ecahother

        //float facing = Math.Abs(Vector3.Angle(transform.forward, player.transform.position));
        //Debug.Log("Facing eachother (the closer to 0 the better)? :" + facing);
        //picValue = picValue * FacingModifier * (1 - facing);

        float facing = Math.Abs(Vector3.Dot(transform.forward, player.transform.position));
        //Debug.Log("Facing eachother (the closer to 0 the better)? :" + facing);
        picValue = picValue * FacingModifier * (1 - facing);



        return picValue;
    }
}
