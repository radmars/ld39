using System.Collections.Generic;
using UnityEngine;

public class Album : MonoBehaviour
{
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
