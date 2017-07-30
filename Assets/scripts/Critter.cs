using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Critter : MonoBehaviour
{
    public string name;
    public double points;

    public double DistanceModifier = .0125;

    public double CenterModifier = .0125;

    public double FacingModifier = 1;
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
        Debug.Log("points for distance :" + (points * (DistanceModifier * (100 - distance))));
        if (distance < 25)
        {
            picValue = picValue + (points * (DistanceModifier * (100 - distance)));
        }

        //how close to center of the screen
        float center = Math.Abs(Vector3.Angle(player.transform.forward, transform.position - player.transform.position));
        Debug.Log("Points for Angle off from center of screen (the closer to 0 the better) :" + (points * CenterModifier * (90 - center)));
        picValue = picValue + (points * CenterModifier * (90 - center));

        //How close you are to facing eachother.
        float facing = Math.Abs(Vector3.Dot(player.transform.forward, transform.forward));
        Debug.Log("Points for facing eachother (the closer to 0 the better)? :" + (points * FacingModifier * (1 - facing)));
        picValue = picValue + (points * FacingModifier * (1 - facing));



        return picValue;
    }
}
