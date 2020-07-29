using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameFinishedMenu : MonoBehaviour {

    public GameObject menuUI;
    public Text uiTextScore;
    public TimeHandler timeHandler;

    void Awake() {
        menuUI.SetActive(false);
    }

    public void Show() {
        menuUI.SetActive(true);
        Time.timeScale = 0f;

        int bonus = timeHandler.GetBonusScore();
        int totalScore = PlayerStats.score + bonus;
        string msg = "";

        if (totalScore > PlayerPrefs.GetInt("highscore"))  {
            msg += "New highscore!\n";
            PlayerPrefs.SetInt("highscore", totalScore);
        }
        uiTextScore.text = string.Format("Bonus: {0}\n", bonus) +
                           string.Format("Total score: {0}\n", totalScore);
    }

    public void PlayAgain() {
        SceneManager.LoadScene("Level1");
    }

}
