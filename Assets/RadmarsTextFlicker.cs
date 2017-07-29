using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RadmarsTextFlicker : MonoBehaviour
{
    public SpriteRenderer mars;
    public SpriteRenderer intro1;
    public SpriteRenderer intro2;
    int counter = 0;

    void Start()
    {
        SetActive(mars);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            FinishIntro();
        }
    }

    void SetActive(SpriteRenderer r) {
        mars.enabled = r == mars;
        intro1.enabled = r == intro1;
        intro2.enabled = r == intro2;
    }

    public void FixedUpdate()
    {
        if (this.counter < 130) SetActive(mars);
        else if (this.counter < 135) SetActive(intro2);
        else if (this.counter < 140) SetActive(intro1);
        else if (this.counter < 145) SetActive(intro2);
        else if (this.counter < 150) SetActive(intro1);
        else if (this.counter < 155) SetActive(intro2);
        else if (this.counter < 160) SetActive(intro1);
        else if (this.counter < 165) SetActive(intro2);
        else SetActive(intro1);
        this.counter++;
        if(counter > 300)
        {
            FinishIntro();
        }
    }

    void FinishIntro()
    {
        SceneManager.LoadScene("the greatest scene", LoadSceneMode.Single);
    }
}
