using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DieMenu : MonoBehaviour {

    public GameObject menuUI;

    void Awake() {
        menuUI.SetActive(false);
    }

    public void PlayerDied() {
        menuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void PlayAgain() {
        SceneManager.LoadScene("Level" + PlayerStats.GameLevel.ToString());
        menuUI.SetActive(false);
        Time.timeScale = 1f;
    }



}
