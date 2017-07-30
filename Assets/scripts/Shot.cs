using UnityEngine;

public struct Shot 
{
    public Critter critter;
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