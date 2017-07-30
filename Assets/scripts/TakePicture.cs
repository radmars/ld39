using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TakePicture : MonoBehaviour
{
    private IEnumerable<Critter> critters;
    private IEnumerable<Critter> visibleCritters;
    private Camera roverCamera;
    private Album album;
    private BatteryAnimator battery;

    void Start()
    {
        album = Album.FindMe();
        battery = GameObject.Find("Battery").GetComponent<BatteryAnimator>();
        roverCamera = GameObject.Find("Rover Camera").GetComponent<Camera>();
        critters = GameObject.FindGameObjectsWithTag("Critter").Select(
            (gameObject) => gameObject.GetComponent<Critter>()
        );
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetButtonDown("Fire1"))
            return;

        battery.PhotoTaken();

        visibleCritters = critters.Where((critter) => IsVisibleFrom(critter.GetComponent<MeshRenderer>(), roverCamera.GetComponent<Camera>()));

        if (visibleCritters.Count() > 0)
        {
            //start with photo as value 0 as "best picture"
            var snapshot = TakeSnapshot(roverCamera.GetComponent<Camera>());
            var visibleCount = visibleCritters.Count();
            Debug.Log("Visible critter count: " + visibleCount);

            var bestShot = visibleCritters.Select((critter) => new Shot()
            {
                critter = critter,
                score = critter.CalculatePoints(gameObject, visibleCount),
                snapshot = snapshot,
            }).OrderByDescending((shot) => shot.score.total).First();

            album.AddShot(bestShot.critter, bestShot);
            Debug.Log("That picture of a " + bestShot.critter.name + " would be worth: " + bestShot.score.total);
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
    }
}