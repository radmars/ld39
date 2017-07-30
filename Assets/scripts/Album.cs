using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Album : MonoBehaviour
{
    // TODO: Repalce INT with picture/metadata
    public Dictionary<string, List<Shot>> shots = new Dictionary<string, List<Shot>>();

    public List<Shot> selected = new List<Shot>();

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

        /*
        selected.Add(new Shot()
        {
            critter = "bob",
            score = new ShotScore()
            {
                total = 123,
                center = 3,
                distance = 4,
                facing = 55,
            }
        });
        selected.Add(new Shot()
        {
            critter = "jim",
            score = new ShotScore()
            {
                total = 1,
                center = 3441,
                distance = 23,
                facing = 23,
            }
        });
        selected.Add(new Shot()
        {
            critter = "stuff",
            score = new ShotScore()
            {
                total = 123,
                center = 1,
                distance = 1,
                facing = 1000,
            }
        });
        selected.Add(new Shot()
        {
            critter = "bzxcv",
            score = new ShotScore()
            {
                total = 123,
                center = 3.33f,
                distance = 4.44f,
                facing = 5555.0f,
            }
        });
        */

        DontDestroyOnLoad(this);
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
    }

    internal void AddShot(string critter, Shot s)
    {
        if (!shots.ContainsKey(critter))
        {
            shots[critter] = new List<Shot>();
        }
        shots[critter].Add(s);
    }
}
