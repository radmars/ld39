using UnityEngine;
using System.Collections;

/// <summary>
/// Class for controling the camera 
/// </summary>
public class CameraControl : MonoBehaviour
{
    /// <summary>
    /// Bool to set showing mouse cursor
    /// </summary>
    public bool ShowCurser;

    /// <summary>
    /// The sensitivity
    /// </summary>
    public float Sensitivity = 1;

    /// <summary>
    /// The start method
    /// </summary>
    void Start()
    {
        if (!ShowCurser)
        {
            Cursor.visible = false;
        }
    }

    private void Awake()
    {
        var camera = GetComponent<Camera>();
        camera.targetTexture.width = 320;
        camera.targetTexture.height= 240;
    }

    /// <summary>
    /// Update method
    /// </summary>
    void Update()
    {
        float newRotationY = transform.localEulerAngles.y + GetFurthestAxis("Horizontal", "Mouse X") * Sensitivity;
        float newRotationX = transform.localEulerAngles.x - GetFurthestAxis("Vertical", "Mouse Y") * Sensitivity;

        gameObject.transform.localEulerAngles = new Vector3(newRotationX, newRotationY, 0);
    }

    float GetFurthestAxis(string a, string b)
    {
        float aa = Input.GetAxis(a);
        float ba = Input.GetAxis(b);
        return (Mathf.Abs(aa) < Mathf.Abs(ba)) ?  ba: aa;
    }
}
