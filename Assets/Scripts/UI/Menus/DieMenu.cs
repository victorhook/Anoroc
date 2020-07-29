using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DieMenu : MonoBehaviour {

    public GameObject menuUI;
    private string level;

    void Awake() {
        menuUI.SetActive(false);
    }

    public void PlayerDied() {
        menuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void PlayAgain() {
        string scene = SceneManager.GetActiveScene().name;
        level = scene[scene.Length - 1].ToString();
        SceneManager.LoadScene("Level" + level);
        menuUI.SetActive(false);
        Time.timeScale = 1f;
    }



}
