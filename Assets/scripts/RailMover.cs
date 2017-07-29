using UnityEngine;
using System.Collections;

public class RailMover : MonoBehaviour
{
    public Rail rail;
    public PlayMode mode;

    public float speed = 2.5f;

    private int currentSeg;
    private float transition;
    
    private bool isCompleted;

    

    private void Update()
    {
        if(!rail)
        return;

        if(!isCompleted)
        Play();
    }

    private void Play()
    {
        float m = (rail.nodes[currentSeg + 1].position - rail.nodes[currentSeg].position).magnitude;
        float s = (Time.deltaTime * 1 / m) * speed;
        transition += s;
        if(transition >1)
        {
            transition=0;
            currentSeg++;

            if (currentSeg == rail.nodes.Length-1)
            {
                isCompleted = true;
                return;
            }
        }
        else if(transition<0)
        {
            transition=1;
            currentSeg--;
        }

        transform.position = rail.PositionOnRail(currentSeg, transition, mode);
        transform.rotation = rail.Orientation(currentSeg, transition);
    }
}