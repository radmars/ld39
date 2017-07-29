using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Album : MonoBehaviour
{
    public ArrowButton left;
    public ArrowButton right;
    public TextMesh nameText;
    public TextMesh shotText;

    // TODO: Repalce INT with picture/metadata
    private Dictionary<string, int[]> shots;
    private Dictionary<string, int> selected;
    private string currentCritter;
    private int[] currentShots;
    private int visibleIndex;

    // Use this for initialization
    void Start()
    {
        selected = new Dictionary<string, int>();
        shots = new Dictionary<string, int[]>
        {
            { "Bubba Feet", new int[] { 1, 2, 3 } },
            { "Robby Mars", new int[] { 1, 2 } }
        };

        ShowNextCritter();
    }

    bool ShowNextCritter()
    {
        if (shots.Count > 0)
        {
            currentCritter = shots.Keys.First();
            currentShots = shots[currentCritter];
            shots.Remove(currentCritter);
            UpdateText();
            return true;
        }
        return false;
    }

    float lastScroll;

    // Update is called once per frame
    void FixedUpdate()
    {
        float x = GetFurthestAxis("Horizontal", "Mouse X");
        if (!Mathf.Approximately(0f, x) && Time.fixedTime - lastScroll > .250)
        {
            Go(x > 0);
            lastScroll = Time.fixedTime;
        }

        if(Input.GetButton("Fire1"))
        {
            selected.Add(currentCritter, currentShots[visibleIndex]);
            if(!ShowNextCritter())
            {
                Debug.Log("SHOW THE SCORE SCREEN");
            }
        }
    }

    void Go(bool next)
    {
        if (next)
        {
            right.Flash();
            visibleIndex++;
        }
        else
        {
            left.Flash();
            visibleIndex--;
        }
        if(visibleIndex < 0 )
        {
            visibleIndex = currentShots.Length - 1;
        }
        visibleIndex %= currentShots.Length;
        UpdateText();
    }

    void UpdateText()
    {
        nameText.text = currentCritter;
        shotText.text = "" + (visibleIndex + 1) + "/" + currentShots.Length;
    }

    float GetFurthestAxis(string a, string b)
    {
        float aa = Input.GetAxis(a);
        float ba = Input.GetAxis(b);
        return (Mathf.Abs(aa) < Mathf.Abs(ba)) ? ba : aa;
    }
}
