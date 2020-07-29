using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using static PlayerController;

public class LevelHandler : MonoBehaviour {

    [SerializeField] private static int experience, nextLevelExp, level;
    [SerializeField] private Text levelText, xpText, levelUpText;
    [SerializeField] private Slider xpSlider;

    [SerializeField] private Animator btnAnimator;
    [SerializeField] private PlayerController player;
    public SFX sfx;

    private Animator levelUpAnimator;
    private static int[] skills;
    private static int unspentPoints;

    public enum Skills {
        speed,
        hitpoints,
        jumpHeight,
        shooting,
        doubleJump,
    };

    // increase values for skill-levelup
    private const float SPEED_INCREASE = 5f;
    private const float JUMP_HEIGHT_INCREASE = 10f;
    private const int HITPOINTS_INCREASE = 20;
    private const float SHOOT_SPEED_INCREASE = .15f;
    // double jump works as follows:
    // skill 1: no doublejump,
    // skill 2: one doublejump,
    // skill 3: two doublejumps

    private void InitSkills() {
        skills        = PlayerStats.skills;
        unspentPoints = PlayerStats.unspentPoints;
        experience    = PlayerStats.experience;
        nextLevelExp  = PlayerStats.nextLevelExp;
        level         = PlayerStats.level;
    }

    public static void SaveStaticVariables() {
        PlayerStats.skills = skills;
        PlayerStats.unspentPoints = unspentPoints;
        PlayerStats.experience = experience;
        PlayerStats.nextLevelExp = nextLevelExp;
        PlayerStats.level = level;
    }

    void Start() {
        InitSkills();
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
        print("Current xp: " + experience.ToString());
        print("xp gained: " + xp.ToString());
        if (experience >= nextLevelExp) {
            LevelUp();
        }

        UpdateUI();
    }


    private void LevelUp() {
        Instantiate(sfx, transform.position, Quaternion.identity);

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
                player.IncreaseShootSpeed(SHOOT_SPEED_INCREASE);
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

