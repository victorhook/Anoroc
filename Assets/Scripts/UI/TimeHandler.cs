﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeHandler : MonoBehaviour {

    private float msCounter;
    private int minutes, seconds;
    [SerializeField] private Text uiText;

    public int bonus = 100;

    void Start()
    {
        msCounter = 0;
    }

    void Update() {
        msCounter += Time.deltaTime;
        if (msCounter >= 1) {
            seconds++;
            if (bonus > 0) {
                bonus--;
            }
            if (seconds == 60) {
                minutes++;
                seconds = 0;
            }
            msCounter = 0;
        }
        uiText.text = string.Format( "Time: " + "{0:00}:{1:00}", minutes, seconds);
    }

    public int GetBonusScore() {
        return bonus;
    }

    public void Reset() {
        minutes = seconds = 0;
    }
}
