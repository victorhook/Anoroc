using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour {

    public AudioSource sfx;

    void Start() {
        float lifeTime = sfx.clip.length;
        Invoke("Die", lifeTime);
        sfx.Play(0);        
    }

    private void Die() {
        Destroy(gameObject);
    }

}
