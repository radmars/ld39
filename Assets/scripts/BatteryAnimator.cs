using UnityEngine;
using UnityEngine.SceneManagement;

public class BatteryAnimator : MonoBehaviour
{

    private SpriteRenderer r;
    public Sprite[] sprites;
    public float timeToDrain = 60 * 5;
    public float startTime = 0;

    public void Start()
    {
        startTime = Time.fixedTime;
        r = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        int spriteIndex = sprites.Length - (int)((Time.fixedTime - startTime) / timeToDrain * sprites.Length) - 1;
        if (spriteIndex >= 0)
        {
            r.sprite = sprites[spriteIndex];
        }
        else
        {
           // SceneManager.LoadScene("shot-selector");
        }
    }
}
