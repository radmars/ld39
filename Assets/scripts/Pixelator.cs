using UnityEngine;
using System.Collections;

public class Pixelator : MonoBehaviour
{

    public RenderTexture renderTexture;
    public RenderTexture uiTexture;

    void OnGUI()
    {
        GUI.depth = 20;
        GUI.DrawTexture(cam.pixelRect, renderTexture);
        GUI.DrawTexture(cam.pixelRect, uiTexture);
    }

    public float _wantedAspectRatio = 1.3333333f;
    static float wantedAspectRatio;
    static Camera cam;
    static Camera backgroundCam;

    void Awake()
    {
        cam = GetComponent<Camera>();
        if (!cam)
        {
            cam = Camera.main;
        }
        if (!cam)
        {
            Debug.LogError("No camera available");
            return;
        }
        wantedAspectRatio = _wantedAspectRatio;
        SetCamera();
    }


    static float oldAspect;
    private void Update()
    {
        if(((float)Screen.width / Screen.height) !=oldAspect)
        {
            SetCamera();
        }
    }

    public static void SetCamera()
    {
        float currentAspectRatio = (float)Screen.width / Screen.height;
        oldAspect = currentAspectRatio;
        // If the current aspect ratio is already approximately equal to the desired aspect ratio,
        // use a full-screen Rect (in case it was set to something else previously)
        if ((int)(currentAspectRatio * 100) / 100.0f == (int)(wantedAspectRatio * 100) / 100.0f)
        {
            cam.rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
            if (backgroundCam)
            {
                Destroy(backgroundCam.gameObject);
            }
            return;
        }
        // Pillarbox
        if (currentAspectRatio > wantedAspectRatio)
        {
            float inset = 1.0f - wantedAspectRatio / currentAspectRatio;
            cam.rect = new Rect(inset / 2, 0.0f, 1.0f - inset, 1.0f);
        }
        // Letterbox
        else
        {
            float inset = 1.0f - currentAspectRatio / wantedAspectRatio;
            cam.rect = new Rect(0.0f, inset / 2, 1.0f, 1.0f - inset);
        }
        if (!backgroundCam)
        {
            // Make a new camera behind the normal camera which displays black; otherwise the unused space is undefined
            backgroundCam = new GameObject("BackgroundCam", typeof(Camera)).GetComponent<Camera>();
            backgroundCam.depth = int.MinValue;
            backgroundCam.clearFlags = CameraClearFlags.SolidColor;
            backgroundCam.backgroundColor = Color.black;
            backgroundCam.cullingMask = 0;
        }
    }
}