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

    // Use this for initialization
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        self = spriteRenderer.sprite;
    }


    internal void Flash()
    {
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
