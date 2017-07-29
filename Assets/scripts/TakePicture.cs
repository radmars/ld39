using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TakePicture : MonoBehaviour
{
    private IEnumerable<Critter> critters;
    private IEnumerable<Critter> visibleCritters;

    void Start()
    {
        critters = GameObject.FindGameObjectsWithTag("Critter").Select(
            (gameObject) => gameObject.GetComponent<Critter>()
        );
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetMouseButtonDown(0) || !Input.GetButtonDown("Fire1"))
            return;

        Debug.Log("Click");
        visibleCritters = critters.Where((critter) => critter.GetComponent<MeshRenderer>().isVisible);

        if (visibleCritters.Count() > 0)
        { //If there are no visible critters
            foreach (var critter in critters)
            {
                Debug.Log(critter);
            }
        }
    }
}