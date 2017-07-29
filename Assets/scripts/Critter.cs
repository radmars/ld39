using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Critter : MonoBehaviour
{
    private void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
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
}
