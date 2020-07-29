using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour {

    void Awake() {
        /* --- Level-related --- */
        PlayerStats.nextLevelExp = 100;
        PlayerStats.experience = 0;
        PlayerStats.level = 1;
        PlayerStats.unspentPoints = 0;

        /* --- Player-skills --- */
        PlayerStats.skills = new int[5] {1,1,1,1,1};
        PlayerStats.hitpoints = 100;
        PlayerStats.jumpForce = 25f;
        PlayerStats.jumpsAllowed = 1;
        PlayerStats.speed = 10f;
        PlayerStats.shootDelay = .3f;

        /* --- Game-&-Highscore-related --- */
        PlayerStats.score = 0;
    }
}
