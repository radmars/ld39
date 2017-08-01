using UnityEngine;
using System.Collections;

public class RailMover : MonoBehaviour
{
    public Rail rail;

    public PlayMode mode;

    public bool isReversed;

    public bool isLooping;

    public bool returns;

    public float speed = 2.5f;
    
    public float minSpeed = 2.5f;

    public float maxSpeed = 5f;

    private int currentSeg;

    private float transition;

    private bool isCompleted;

    public bool grounded;



    private void Update()
    {
        if (!rail)
            return;

        if (!isCompleted)
            Play(!isReversed);

        if(name == "Rover" && Input.GetButton("Fire2"))
        { 
            if(speed < maxSpeed)
            {
                speed += .1f;
            }            
        }
        else{
            if(speed >minSpeed)
            {
                speed -=0.1f;
            }
        }
    }

    private void Play(bool forward = true)
    {
        //calculate speed and stuff
        float m = (rail.nodes[currentSeg + 1].position - rail.nodes[currentSeg].position).magnitude;
        float s = (Time.deltaTime * 1 / m) * speed;
        transition += (forward) ? s : -s;
        //check transition
        if (transition > 1)
        {
            transition = 0;
            currentSeg++;
            //if we reached the end
            if (currentSeg == rail.nodes.Length - 1)
            {
                //if we're looping
                if (isLooping)
                {
                    //if we comin home bby
                    if(returns)
                    {
                        transition = 1;
                        currentSeg = rail.nodes.Length -2;
                        isReversed = !isReversed;
                    }
                    else{
                        currentSeg=0;
                    }
                }
                else
                {
                    isCompleted = true;
                    return;
                }

            }
        }
        else if (transition < 0)
        {
            transition = 1;
            currentSeg--;

            if (currentSeg == -1)
            {
                if (isLooping)
                {
                    if(returns)
                    {
                        transition = 0;
                        currentSeg = 0;
                        isReversed = !isReversed;
                    }
                    else{
                        currentSeg= rail.nodes.Length-2;
                    }
                }
                else
                {
                    isCompleted = true;
                    return;
                }
            }
        }

        transform.position = rail.PositionOnRail(currentSeg, transition, mode, grounded);
        transform.rotation = rail.Orientation(currentSeg, transition, isReversed);
    }
}