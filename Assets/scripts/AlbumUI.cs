using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AlbumUI : MonoBehaviour
{
    public ArrowButton left;
    public ArrowButton right;
    public TextMesh nameText;
    public TextMesh shotText;
    private Album album;

    private string currentCritter;
    private int[] currentShots;
    private int visibleIndex;

    // Use this for initialization
    void Start()
    {
        album = Album.FindMe();
        ShowNextCritter();
    }

    bool ShowNextCritter()
    {
        var shots = album.shots;
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

        if(Input.GetButtonDown("Fire1"))
        {
            album.selected.Add(currentCritter, currentShots[visibleIndex]);
            if(!ShowNextCritter())
            {
                SceneManager.LoadScene("score-screen");
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
