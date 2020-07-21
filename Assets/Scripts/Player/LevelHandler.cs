using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using static PlayerController;

public class LevelHandler : MonoBehaviour
{
    [SerializeField] private int experience, nextLevelExp, level;
    [SerializeField] private Text levelText, xpText, levelUpText;
    [SerializeField] private Slider xpSlider;

    [SerializeField] private Animator btnAnimator;
    [SerializeField] private PlayerController player;

    private Animator levelUpAnimator;
    private int[] skills;
    private int unspentPoints;

    public enum Skills {
        speed,
        hitpoints,
        jumpHeight,
        shooting,
        doubleJump,
    };

    // increase values for skill-levelup
    private const float SPEED_INCREASE = 10f;
    private const float JUMP_HEIGHT_INCREASE = 10f;
    private const int HITPOINTS_INCREASE = 20;
    private const int DAMAGE_INCREASE = 5;
    // double jump works as follows:
    // skill 1: no doublejump,
    // skill 2: one doublejump,
    // skill 3: two doublejumps

    void Awake() {
        skills = new int[5] {1, 1, 1, 1, 1};
    }

    void Start() {
        unspentPoints  = 0;
        experience     = 0;
        nextLevelExp   = 100;
        level          = 1;

        xpSlider.minValue = 0;
        xpSlider.maxValue = nextLevelExp;

        levelUpAnimator = levelUpText.GetComponent<Animator>();

        UpdateUI();
    }

    private void UpdateUI() {
        levelText.text = "Level: " + level.ToString();
        xpText.text = "XP: " + experience.ToString();
        xpSlider.value = experience;
    }

    /* experience gained! */
    public void Increase(int xp) {
        experience += xp;

        if (experience >= nextLevelExp) {
            LevelUp();
        }
        
        UpdateUI();
    }
    

    private void LevelUp() {

        levelUpAnimator.SetTrigger("LevelUp");

        // get remainder of exp for next level
        experience %= nextLevelExp;

        // increase level and xp needed for next level
        level++;
        nextLevelExp *= 2;

        // update slider max value
        xpSlider.maxValue = nextLevelExp;

        unspentPoints++;

        btnAnimator.SetBool("skillLevelupAvailable", true);
    }

    public bool CanSkillLevelUp(Skills skill) {
         return unspentPoints > 0 && skills[(int) skill] < 3;
     }
     
    public void SkillLevelUp(Skills skill) {
        skills[(int) skill]++;
        unspentPoints--;

        // update correct skill on player
        switch ((int) skill) {
            case (int) Skills.speed: 
                player.IncreaseSpeed(SPEED_INCREASE);
                break;
            case (int) Skills.hitpoints:
                player.IncreaseHp(HITPOINTS_INCREASE);
                break;
            case (int) Skills.jumpHeight:
                player.IncreaseJumpHeight(JUMP_HEIGHT_INCREASE);
                break;
            case (int) Skills.shooting:
                player.IncreaseDamage(DAMAGE_INCREASE);
                break;
            case (int) Skills.doubleJump:
                player.IncreaseDoubleJump();
                break;
        }

        // check if we should stop "skill-availalbe-animation"
        if (unspentPoints == 0) {
            btnAnimator.SetBool("skillLevelupAvailable", false);
        }
    }

    public int GetLevel(Skills skill) {
        return skills[(int) skill];
    }

}

