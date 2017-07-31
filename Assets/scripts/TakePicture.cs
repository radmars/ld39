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
    private Vector3 cameraDefaultLoc;
    public AudioSource audioSource;
    public AudioClip cameraSound;

    private bool isSelfieMode = false;

    void Start()
    {
        album = Album.FindMe();
        battery = GameObject.Find("Battery").GetComponent<BatteryAnimator>();
        roverCamera = GameObject.Find("Rover Camera").GetComponent<Camera>();
        cameraDefaultLoc = roverCamera.transform.localPosition;
        critters = GameObject.FindGameObjectsWithTag("Critter").Select(
            (gameObject) => gameObject.GetComponent<Critter>()
        );
    }

    // Update is called once per frame
    void Update()
    {
        bool selfie = false;
        if (Input.GetButtonDown("Fire3"))
            isSelfieMode = !isSelfieMode;

        if(isSelfieMode)
        {
            Debug.Log("should move camera Local rotation y is:  " + roverCamera.transform.localEulerAngles.y);
            if (roverCamera.transform.localPosition.z + 0.4f < cameraDefaultLoc.z + 3.1f)
                roverCamera.transform.localPosition = new Vector3(roverCamera.transform.localPosition.x, roverCamera.transform.localPosition.y, roverCamera.transform.localPosition.z + .04f);
            if (roverCamera.transform.localEulerAngles.y < 170f)
            {
                Debug.Log("should move positive camera turn");
                roverCamera.transform.localEulerAngles = new Vector3(roverCamera.transform.localEulerAngles.x, roverCamera.transform.localEulerAngles.y + 2f, roverCamera.transform.localEulerAngles.z);
            }
            else if (roverCamera.transform.localEulerAngles.y > 190f)
            {
                Debug.Log("should move negative camera turn");
                roverCamera.transform.localEulerAngles = new Vector3(roverCamera.transform.localEulerAngles.x, roverCamera.transform.localEulerAngles.y - 2f, roverCamera.transform.localEulerAngles.z);
            }
            else{
                selfie=true;
            }
        }
        else
        {
            if (roverCamera.transform.localPosition.z - 0.4f > cameraDefaultLoc.z)
            {
                roverCamera.transform.localPosition = new Vector3(roverCamera.transform.localPosition.x, roverCamera.transform.localPosition.y, roverCamera.transform.localPosition.z - .04f);

                if (roverCamera.transform.localEulerAngles.y < 350f && roverCamera.transform.localEulerAngles.y >= 180f)
                {
                    Debug.Log("should move positive camera turn");
                    roverCamera.transform.localEulerAngles = new Vector3(roverCamera.transform.localEulerAngles.x, roverCamera.transform.localEulerAngles.y + 2f, roverCamera.transform.localEulerAngles.z);
                }
                if (roverCamera.transform.localEulerAngles.y > 10f && roverCamera.transform.localEulerAngles.y <180f)
                {
                    Debug.Log("should move negative camera turn");
                    roverCamera.transform.localEulerAngles = new Vector3(roverCamera.transform.localEulerAngles.x, roverCamera.transform.localEulerAngles.y - 2f, roverCamera.transform.localEulerAngles.z);
                }
            }
        }

        if (!Input.GetButtonDown("Fire1"))
            return;

        audioSource.Stop();
        audioSource.PlayOneShot(cameraSound);

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
                critter = critter.name,
                score = critter.CalculatePoints(gameObject, visibleCount, selfie),
                snapshot = snapshot,
            }).OrderByDescending((shot) => shot.score.total).First();

            album.AddShot(bestShot.critter, bestShot);
            Debug.Log("That picture of a " + bestShot.critter + " would be worth: " + bestShot.score.total);
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