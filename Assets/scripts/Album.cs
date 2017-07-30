using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Album : MonoBehaviour
{
    // TODO: Repalce INT with picture/metadata
    public Dictionary<string, List<Shot>> shots = new Dictionary<string, List<Shot>>();

    public Dictionary<string, Shot> selected = new Dictionary<string, Shot>();

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

    internal void AddShot(Critter critter, Shot s)
    {
        if(!shots.ContainsKey(critter.name))
        {
            shots[critter.name] = new List<Shot>();
        }
        shots[critter.name].Add(s);
    }
}
