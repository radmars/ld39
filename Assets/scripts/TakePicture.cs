using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine.SceneManagement;

public class TakePicture : MonoBehaviour
{
    private IEnumerable<Critter> critters;
    private IEnumerable<Critter> visibleCritters;
    private GameObject camera;
    private Album album;

    private int pictureCount = 0;

    void Start()
    {
        album = Album.FindMe();
        camera = GameObject.Find("Rover Camera");
        critters = GameObject.FindGameObjectsWithTag("Critter").Select(
            (gameObject) => gameObject.GetComponent<Critter>()
        );
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetButtonDown("Fire1"))
            return;

        Debug.Log("Click");
        pictureCount++;

        visibleCritters = critters.Where((critter) => IsVisibleFrom(critter.GetComponent<MeshRenderer>(), camera.GetComponent<Camera>()));


        if (visibleCritters.Count() > 0)
        {
            //start with photo as value 0 as "best picture"
            var bestCrit = visibleCritters.First();
            var bestShot = new Shot();
            bestShot.snapshot = TakeSnapshot(camera.GetComponent<Camera>());
            bestShot.value = 0;
            Debug.Log(pictureCount + ": Visible critter count: " + visibleCritters.Count());

            //Iterate through each one and see which picture is the best
            foreach (var critter in visibleCritters)
            {
                var s = new Shot();
                s.value = critter.CalculatePoints(gameObject, visibleCritters.Count());
                if (s.value > bestShot.value)
                {
                    bestCrit = critter;
                    bestShot.value = s.value;
                }
            }

            album.AddShot(bestCrit, bestShot);
            //SceneManager.LoadScene("shot-selector");
            Debug.Log("That picture of a " + bestCrit.name + " would be worth: " + bestCrit.CalculatePoints(camera, visibleCritters.Count()) + " points");
        }
    }

    private Texture2D TakeSnapshot(Camera camera)
    {
        RenderTexture.active = camera.targetTexture;
        Texture2D text = new Texture2D(RenderTexture.active.width, RenderTexture.active.height);
        text.ReadPixels(new Rect(0, 0, text.width, text.height), 0, 0);
        text.Apply();
        RenderTexture.active = null;
        return text;
    }

    public bool IsVisibleFrom(Renderer renderer, Camera camera)
    {
        Ray testHitRay = new Ray(camera.transform.position, (renderer.transform.position - camera.transform.position).normalized);
        RaycastHit hit;
        Physics.Raycast(testHitRay, out hit, 500f);
        if (hit.transform.gameObject.tag == "Critter")
        {
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
            return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
        }

        return false;
        //
    }
}