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
    private AudioSource music;

    public void Start()
    {
        music = GameObject.Find("Musicbox").GetComponent<AudioSource>();
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
        float totalEnergyUsePercent = (
                totalEnergyDrain
                + photos * photoEnergyCost
            ) / totalEnergy;
        int spriteIndex = sprites.Length - (int)(
             totalEnergyUsePercent * sprites.Length) - 1;

        if (totalEnergyUsePercent > .9f)
        {
            music.pitch = (1.0f - totalEnergyUsePercent) * 5.0f + .5f;
        }

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
