using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour {

    void Awake() {
        /* --- Level-related --- */
        PlayerStats.NextLevelExp = 100;
        PlayerStats.Experience = 0;
        PlayerStats.Level = 1;
        PlayerStats.UnspentPoints = 0;

        /* --- Player-skills --- */
        PlayerStats.Skills = new int[5] {1,1,1,1,1};
        PlayerStats.Hitpoints = 100;
        PlayerStats.JumpForce = 25f;
        PlayerStats.JumpsAllowed = 1;
        PlayerStats.Speed = 10f;
        PlayerStats.ShootDelay = .3f;

        /* --- Game-&-Highscore-related --- */
        PlayerStats.Score = 0;
        PlayerStats.GameLevel = 1;
    }
}
