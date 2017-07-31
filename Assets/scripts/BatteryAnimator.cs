using UnityEngine;
using UnityEngine.SceneManagement;

public class BatteryAnimator : MonoBehaviour
{

    private SpriteRenderer r;
    public Sprite[] sprites;
    public float totalEnergy = 60 * 5;
    public float startTime = 0;
    public float lastTime = 0;
    public int photos = 0;
    public float photoEnergyCost = 3;
    public float energyPerSecond = 1;
    public bool singing = false;
    private float singDrainAmount = 11;
    private float totalEnergyDrain = 0;

    public void Start()
    {
        startTime = Time.fixedTime;
        lastTime = startTime;
        r = GetComponent<SpriteRenderer>();
    }

    public void PhotoTaken()
    {
        photos++;
    }

    private float getSingDrain()
    {
        return singing ? singDrainAmount : 0;
    }

    private void FixedUpdate()
    {
        totalEnergyDrain += (Time.fixedTime - lastTime) * (energyPerSecond + getSingDrain());
        lastTime = Time.fixedTime;
        int spriteIndex = sprites.Length - (int)(
            (
                totalEnergyDrain
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
