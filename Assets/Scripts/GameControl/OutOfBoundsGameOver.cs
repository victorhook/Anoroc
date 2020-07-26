using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfMapGameOver : MonoBehaviour {
    void OnCollisionEnter2D(Collision2D collider) {
        if (collider.gameObject.name == "Player") {
            
        }
    }
}
