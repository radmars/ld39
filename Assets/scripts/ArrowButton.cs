using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowButton : MonoBehaviour
{

    private Sprite self;
    public Sprite flash;
    private SpriteRenderer spriteRenderer;
    private int flashid;
    public AudioClip tickSound;
    public AudioSource source;

    // Use this for initialization
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        self = spriteRenderer.sprite;
    }


    internal void Flash()
    {
        source.Stop();
        source.PlayOneShot(tickSound);
        StartCoroutine(_Flash());
    }

    private IEnumerator _Flash()
    {
        spriteRenderer.sprite = flash;
        var i = ++flashid;
        yield return new WaitForSeconds(0.05f);
        if (flashid == i)
        {
            spriteRenderer.sprite = self;
        }
    }
}
