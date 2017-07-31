using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Birthday : MonoBehaviour {

    private BatteryAnimator battery;
    public AudioSource audioSource;
    public AudioClip birthdaySound;
    private bool singing = false;
    private bool started = false;

    void Start () {
        battery = GameObject.Find("Battery").GetComponent<BatteryAnimator>();
    }
	
	void Update () {
        if (Input.GetButton("Fire4")) {
            if (!singing) {
                singing = true;
                battery.singing = true;
                if (started) {
                    audioSource.UnPause();
                }
                else {
                    audioSource.PlayOneShot(birthdaySound);
                    started = true;
                }
            }
        } else {
            singing = false;
            battery.singing = false;
            audioSource.Pause();
        }

    }
}
