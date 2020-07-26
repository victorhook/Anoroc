using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour {

    public DieMenu dieMenu;

    void OnCollisionEnter2D(Collision2D collider) {
        if (collider.gameObject.name == "Player") {
            dieMenu.PlayerDied();
        }
    }
}
