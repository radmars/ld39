using UnityEngine;

public struct Shot 
{
    public string critter;
    public Texture2D snapshot;
    public ShotScore score;
}

public struct ShotScore
{
    public float total;
    internal float distance;
    internal float center;
    internal float facing;
}