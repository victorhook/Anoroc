using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStats {

    /* --- Level --- */
    private static int experience, nextLevelExp, level;
    private static int unspentPoints;

    /* --- Player-skills --- */
    private static float speed, jumpForce, shootDelay;
    private static int hitpoints, jumpsAllowed;
    private static int[] skills;

    /* --- Game-&-Highscore-related --- */
    private static int score;
    private static int gameLevel;


    public static float Speed {
        get {
            return speed;
        }
        set {
            speed = value;
        }
    }

    public static float JumpForce {
        get {
            return jumpForce;
        }
        set {
            jumpForce = value;
        }
    }

    public static float ShootDelay {
        get {
            return shootDelay;
        }
        set {
            shootDelay = value;
        }
    }

    public static int JumpsAllowed {
        get {
            return jumpsAllowed;
        }
        set {
            jumpsAllowed = value;
        }
    }

    public static int Experience {
        get {
            return experience;
        }
        set {
            experience = value;
        }
    }

    public static int NextLevelExp {
        get {
            return nextLevelExp;
        }
        set {
            nextLevelExp = value;
        }
    }

    public static int Level {
        get {
            return level;
        }
        set {
            level = value;
        }
    }

    public static int UnspentPoints {
        get {
            return unspentPoints;
        }
        set {
            unspentPoints = value;
        }
    }

    public static int Score {
        get {
            return score;
        }
        set {
            score = value;
        }
    }

    public static int GameLevel {
        get {
            return gameLevel;
        }
        set {
            gameLevel = value;
        }
    }

    public static int Hitpoints {
        get {
            return hitpoints;
        }
        set {
            hitpoints = value;
        }
    }

    public static int[] Skills {
        get {
            return skills;
        }
        set {
            skills = value;
        }
    }

}
