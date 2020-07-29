using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHandler : MonoBehaviour {

    [SerializeField] private Text uiText;

    private static int score;

    void Start() {
        print(PlayerStats.score);
        score = PlayerStats.score;
        UpdateUI();
    }

    public void Increase(int ammount) {
        score += ammount;
        UpdateUI();
    }

    public void Reset() {
        //score = 0;
    }

    private void UpdateUI() {
        uiText.text = "Score: " + score.ToString();
    }

    public static void SaveStaticVariables() {
        PlayerStats.score = score;
    }

}


