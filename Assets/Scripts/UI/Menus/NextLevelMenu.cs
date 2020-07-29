using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NextLevelMenu : MonoBehaviour {

    public GameObject menuUI;
    public Text uiTextTitle;
    public Text uiTextScore;
    public TimeHandler timeHandler;

    private string level;
    private int score, bonus;

    void Awake() {
        menuUI.SetActive(false);
    }

    public void LevelComplete() {

        menuUI.SetActive(true);
        Time.timeScale = 0f;

        PlayerController.SaveStaticVariables();
        ScoreHandler.SaveStaticVariables();
        LevelHandler.SaveStaticVariables();
        PlayerStats.score += bonus;

        string scene = SceneManager.GetActiveScene().name;
        level = scene[scene.Length - 1].ToString();
        bonus = timeHandler.GetBonusScore();

        uiTextTitle.text = string.Format("Level {0} complete\n", level);
        uiTextScore.text = string.Format("Bonus: {0}\n", bonus) +
                           string.Format("Total score: {0}", PlayerStats.score);
    }


    public void NextLevel() {
        SceneManager.LoadScene("Level" + (Int32.Parse(level) + 1).ToString());
        menuUI.SetActive(false);
        Time.timeScale = 1f;
    }

}
