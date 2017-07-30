using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTable : MonoBehaviour
{
    private Album album;
    public TextMesh scoreTableText;
    public TextMesh totalText;

    void Awake()
    {
        album = Album.FindMe();
        var finalText = "";
        int total = 0;
        foreach (var critter in album.selected.Keys)
        {
            finalText += critter + ": $" + album.selected[critter] + "\n";
            total += album.selected[critter];
        }
        scoreTableText.text = finalText;
        totalText.text = "" + total;
    }

}
