using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Album : MonoBehaviour
{
    // TODO: Repalce INT with picture/metadata
    public Dictionary<string, int[]> shots = new Dictionary<string, int[]>
        {
            { "Bubba Feet", new int[] { 1, 2, 3 } },
            { "Robby Mars", new int[] { 1, 2 } }
        };

    public Dictionary<string, int> selected = new Dictionary<string, int>();

    public static Album FindMe()
    {
        return FindObjectOfType<Album>();
    }

    public void Reset()
    {
        shots.Clear();
        selected.Clear();
    }

    public void Awake()
    {
        DontDestroyOnLoad(this);
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
    }
}
