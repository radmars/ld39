using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Critter : MonoBehaviour
{
    public string name;
    public double points;
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

       var distance = Vector3.Distance(transform.position, player.transform.position);
        
        Debug.Log(distance);
        
           
            picValue = picValue * (.02 * (100 - distance));
        
        return picValue;
    }
}
