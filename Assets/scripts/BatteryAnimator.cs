using UnityEngine;
using UnityEngine.SceneManagement;

public class BatteryAnimator : MonoBehaviour
{

    private SpriteRenderer r;
    public Sprite[] sprites;
    public float totalEnergy = 60 * 5;
    public float startTime = 0;
    public int photos = 0;
    public float photoEnergyCost = 3;
    public float energyPerSecond = 1;

    public void Start()
    {
        startTime = Time.fixedTime;
        r = GetComponent<SpriteRenderer>();
    }

    public void PhotoTaken()
    {
        photos++;
    }

    private void FixedUpdate()
    {
        int spriteIndex = sprites.Length - (int)(
            (
                (Time.fixedTime - startTime) * energyPerSecond
                + photos * photoEnergyCost
            ) / totalEnergy * sprites.Length) - 1;

        if (spriteIndex >= 0)
        {
            r.sprite = sprites[spriteIndex];
        }
        else
        {
            SceneManager.LoadScene("shot-selector");
        }
    }
}
