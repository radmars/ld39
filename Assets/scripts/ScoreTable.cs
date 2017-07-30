using UnityEngine;
using System.Linq;

public class ScoreTable : MonoBehaviour
{
    private Album album;
    public TextMesh nameText;
    public TextMesh scoreTableText;
    public TextMesh totalText;
    public ArrowButton left;
    public ArrowButton right;
    public SpriteRenderer snapshotRenderer;

    private int visibleIndex;
    private Shot current;

    public AudioClip[] clips;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        album = Album.FindMe();
        var finalText = "";
        double total = 0;
        foreach (var shot in album.selected)
        {
            finalText += shot.critter + ": $" + shot.score.total + "\n";
            total += shot.score.total;
        }
        scoreTableText.text = finalText;
        totalText.text = "" + total;

        var shots = album.selected;
        visibleIndex = 0;
        if (shots.Count > 0)
        {
            current = shots[visibleIndex];
            UpdateScreen();
        }
    }

    float lastScroll;

    void Update()
    {
        float x = GetFurthestAxis("Horizontal", "Mouse X");
        if (!Mathf.Approximately(0f, x) && Time.fixedTime - lastScroll > .250)
        {
            Go(x > 0);
            lastScroll = Time.fixedTime;
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
            visibleIndex = album.selected.Count() - 1;
        }

        audioSource.Stop();
        audioSource.PlayOneShot(clips[Random.Range(0, clips.Length)]);

        visibleIndex %= album.selected.Count();
        current = album.selected[visibleIndex];
        UpdateScreen();
    }

    void UpdateScreen()
    {
        var text = current.snapshot;
        if (text)
        {
            snapshotRenderer.sprite = Sprite.Create(text, new Rect(0, 0, text.width, text.height), new Vector2(0.5f, 0.5f));
            snapshotRenderer.sprite.texture.filterMode = FilterMode.Point;
        }
        nameText.text = current.critter;
        scoreTableText.text = "SHOT RECIPT: \n";
        scoreTableText.text += "CENTERED: " + current.score.center + "\n";
        scoreTableText.text += "DISTANCE: " + current.score.distance + "\n";
        scoreTableText.text += "FACING: " + current.score.facing + "\n";
        scoreTableText.text += "SELFIE: " + " NOT YET " + "\n";
        scoreTableText.text += "TOTAL: " + current.score.total;
    }

    float GetFurthestAxis(string a, string b)
    {
        float aa = Input.GetAxis(a);
        float ba = Input.GetAxis(b);
        return (Mathf.Abs(aa) < Mathf.Abs(ba)) ? ba : aa;
    }
}
