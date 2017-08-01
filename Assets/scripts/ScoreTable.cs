using UnityEngine;
using System.Linq;
using System.Collections;
using UnityEngine.SceneManagement;

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
    private float startTime;

    void Awake()
    {
        startTime = Time.fixedTime;
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
        totalText.text = "TOTAL: " + total.ToString("0.00");

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
        if(Input.GetButtonDown("Fire2") && Time.fixedTime - startTime > 1.5)
        {
            SceneManager.LoadScene("splash-menu");
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

        StartCoroutine(PlayGreatSound());

        visibleIndex %= album.selected.Count();
        current = album.selected[visibleIndex];
        UpdateScreen();
    }

    IEnumerator PlayGreatSound()
    {
        yield return new WaitForSeconds(0.1f);
        audioSource.Stop();
        audioSource.PlayOneShot(clips[Random.Range(0, clips.Length)]);
    }

    void UpdateScreen()
    {
        var text = current.snapshot;
        if (text)
        {
            snapshotRenderer.sprite = Sprite.Create(text, new Rect(0, 0, text.width, text.height), new Vector2(0.5f, 0.5f));
            snapshotRenderer.sprite.texture.filterMode = FilterMode.Point;
        }
        nameText.text = current.critter.ToUpper();
        scoreTableText.text = "SHOT SUMMARY:  \n";
        scoreTableText.text += " CENTERED: \n  " + current.score.center.ToString("0.00") + "\n";
        scoreTableText.text += " DISTANCE: \n  " + current.score.distance.ToString("0.00") + "\n";
        scoreTableText.text += " FACING: \n  " + current.score.facing.ToString("0.00") + "\n";
        scoreTableText.text += " SELFIE: \n  " + current.score.selfie.ToString() + "\n";
        scoreTableText.text += " TOTAL: \n  " + current.score.total.ToString("0.00");
        scoreTableText.text = scoreTableText.text.ToUpper();
    }

    float GetFurthestAxis(string a, string b)
    {
        float aa = Input.GetAxis(a);
        float ba = Input.GetAxis(b);
        return (Mathf.Abs(aa) < Mathf.Abs(ba)) ? ba : aa;
    }
}
