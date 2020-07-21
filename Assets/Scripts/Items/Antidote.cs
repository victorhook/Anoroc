using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Antidote : PointItem {

    public ParticleSystem particleSystem;

    void Start() {
        particleSystem.Play();
    }

}
