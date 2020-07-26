using UnityEngine;
using UnityEngine.UI;

public class Highscores : MonoBehaviour {

    public GameObject menuUI;
    public MainMenu mainMenu;
    public Text uiText;

    void Awake()  {
        menuUI.SetActive(false);
    }

    public void Show() {
        menuUI.SetActive(true);
        int score = PlayerPrefs.GetInt("highscore");
        uiText.text = string.Format("Best: {0}", score);
    }

    public void Back() {
        menuUI.SetActive(false);
        mainMenu.Show();
    }

}
