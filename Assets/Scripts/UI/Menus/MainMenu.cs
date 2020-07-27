using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameObject menuUI;
    public Highscores highscores;

    public void Play() {
        SceneManager.LoadScene("Level1");
    }

    public void ShowHighscores() {
        PlayerPrefs.SetInt("highscore", 100);
        menuUI.SetActive(false);
        highscores.Show();
    }

    public void Show() {
        menuUI.SetActive(true);
    }

    public void Quit() {
        Application.Quit();
    }

}
