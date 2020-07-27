using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour {

    public AudioSource backgroundSong;
    public Slider volumeSlider;
    public SpriteRenderer muteSymbol;

    private float lastVolumeValue;
    
    void Awake() {
        UpdateVolume();
    }

    void Start() {
        backgroundSong.Play();
    }

    void Update() {
        backgroundSong.volume = volumeSlider.value;

        if (backgroundSong.volume == 0) {
            muteSymbol.enabled = true;
        } else {
            muteSymbol.enabled = false;
        }
    }

    public void Pause() {
        backgroundSong.Pause();
    }

    public void Resume() {
        backgroundSong.Play();
    }

    public void Mute() {
        if (volumeSlider.value != 0) {
            lastVolumeValue = volumeSlider.value;
            volumeSlider.value = 0;
        } else {
            volumeSlider.value = lastVolumeValue;
        } 
    }

    public void UpdateVolume() {
        volumeSlider.value = PlayerPrefs.GetFloat("music");
    }

    public float GetSFXVolume() {
        return PlayerPrefs.GetFloat("sfx");
    }

}
