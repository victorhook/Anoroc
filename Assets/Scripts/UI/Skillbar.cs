﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skillbar : MonoBehaviour
{

    public Slider slider;
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = 100;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q)) {
            slider.value = 10;
        }

    }
}
