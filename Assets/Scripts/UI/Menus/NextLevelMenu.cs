using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NextLevelMenu : MonoBehaviour {

    public GameObject menuUI;
    public Text uiText;

    void Awake() {
        menuUI.SetActive(false);
    }

    public void LevelComplete() {
        menuUI.SetActive(true);
        Time.timeScale = 0f;
        uiText.text = string.Format("Level {0} complete\n", PlayerStats.GameLevel) + 
                      string.Format("Total score: {0}", PlayerStats.Score);

        PlayerStats.GameLevel++;
    }

    public void NextLevel() {
        print("Next level!");
        SceneManager.LoadScene("Level" + PlayerStats.GameLevel.ToString());
        menuUI.SetActive(false);
        Time.timeScale = 1f;
    }



}
