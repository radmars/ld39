using UnityEngine;
using System.Collections;

public class Pixelator : MonoBehaviour
{

    public RenderTexture renderTexture;
    public RenderTexture uiTexture;

    void OnGUI()
    {
        GUI.depth = 20;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), renderTexture);
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), uiTexture);
    }
}