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
    
    /// <summary>
    /// Update method
    /// </summary>
    void Update()
    {
        float newRotationY = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * Sensitivity;
        float newRotationX = transform.localEulerAngles.x - Input.GetAxis("Mouse Y") * Sensitivity;

        gameObject.transform.localEulerAngles = new Vector3(newRotationX, newRotationY, 0);
    }
}
