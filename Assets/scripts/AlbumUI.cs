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
    public SpriteRenderer snapshotRenderer;
    private Album album;
    public AudioSource source;
    public AudioClip selectSound;

    private string currentCritter;
    private List<Shot> currentShots;
    private int visibleIndex;
    float startTime;

    // Use this for initialization
    void Start()
    {
        album = Album.FindMe();
        ShowNextCritter();
        startTime = Time.fixedTime;
    }

    bool ShowNextCritter()
    {
        var shots = album.shots;
        visibleIndex = 0;
        if (shots.Count > 0)
        {
            currentCritter = shots.Keys.First();
            currentShots = shots[currentCritter];
            shots.Remove(currentCritter);
            UpdateScreen();
            return true;
        }
        return false;
    }

    float lastScroll;

    // Update is called once per frame
    void Update()
    {
        float x = GetFurthestAxis("Horizontal", "Mouse X");
        if (!Mathf.Approximately(0f, x) && Time.fixedTime - lastScroll > .250)
        {
            Go(x > 0);
            lastScroll = Time.fixedTime;
        }

        if (Input.GetButtonDown("Fire1") && Time.fixedTime - startTime > 1.5)
        {
            source.Stop();
            source.PlayOneShot(selectSound);
            album.selected.Add(currentShots[visibleIndex]);
            if (!ShowNextCritter())
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
        if (visibleIndex < 0)
        {
            visibleIndex = currentShots.Count() - 1;
        }
        visibleIndex %= currentShots.Count();
        UpdateScreen();
    }

    void UpdateScreen()
    {
        var text = currentShots[visibleIndex].snapshot;
        snapshotRenderer.sprite = Sprite.Create(text, new Rect(0, 0, text.width, text.height), new Vector2(0.5f, 0.5f));
        snapshotRenderer.sprite.texture.filterMode = FilterMode.Point;
        nameText.text = currentCritter.ToUpper();
        shotText.text = "" + (visibleIndex + 1) + "/" + currentShots.Count();
    }

    float GetFurthestAxis(string a, string b)
    {
        float aa = Input.GetAxis(a);
        float ba = Input.GetAxis(b);
        return (Mathf.Abs(aa) < Mathf.Abs(ba)) ? ba : aa;
    }
}
