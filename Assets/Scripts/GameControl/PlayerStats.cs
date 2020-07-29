using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStats {

    /* --- Level --- */
    public static int experience, nextLevelExp, level;
    public static int unspentPoints;

    /* --- Player-skills --- */
    public static float speed, jumpForce, shootDelay;
    public static int hitpoints, jumpsAllowed;
    public static int[] skills;

    /* --- Game-&-Highscore-related --- */
    public static int score;
}
