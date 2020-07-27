using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour {

    public AudioSource sfx;
    private AudioController audioController;

    void Awake() {
        audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
    }

    void Start() {
        sfx.volume = audioController.GetSFXVolume();
        float lifeTime = sfx.clip.length;
        //Invoke("Die", lifeTime);
        Invoke("Die", 5);
        sfx.Play(0);        
    }

    private void Die() {
        Destroy(gameObject);
    }

}
