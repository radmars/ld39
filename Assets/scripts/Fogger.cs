using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fogger : MonoBehaviour {
    public float density = .15f;
    public float start = 20;
    public float end = 180;
    public Color color = new Color(0xdf, 0xa5, 0x00);
    // Use this for initialization
    void Start() {
        Debug.Log("Fog it up");
        RenderSettings.fog = true;
        RenderSettings.fogMode = FogMode.Linear;
        RenderSettings.fogColor = color;
    }

    public void Update()
    {
        RenderSettings.fogStartDistance = start;
        RenderSettings.fogEndDistance = end;
        RenderSettings.fogDensity = density;
	}
}
