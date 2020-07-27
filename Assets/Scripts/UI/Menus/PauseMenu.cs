using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public static bool gameIsPaused;

    public GameObject pauseMenuUI;
    public GameObject settingsUI;

    public Slider musicSlider;
    public Slider sfxSlider;

    public AudioController audioController;

    void Awake()  {
        settingsUI.SetActive(false);
        gameIsPaused = false;
        Resume();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {

            if (gameIsPaused) {
                if (settingsUI.activeSelf) {
                    SaveSettings();
                } else {
                    Resume();
                }
            } else {
                Pause();
            }
        }
    }

    public void Resume() {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        audioController.Resume();
    }

    /* opens the pause-UI and freezes game time */
    public void Pause() {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
        audioController.Pause();
    }

    public void MainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void Settings() {
        musicSlider.value = PlayerPrefs.GetFloat("music");
        sfxSlider.value = PlayerPrefs.GetFloat("sfx");
        pauseMenuUI.SetActive(false);
        settingsUI.SetActive(true);
    }

    public void SaveSettings() {
        PlayerPrefs.SetFloat("music", musicSlider.value);
        PlayerPrefs.SetFloat("sfx", sfxSlider.value);
        settingsUI.SetActive(false);
        pauseMenuUI.SetActive(true);

        audioController.UpdateVolume();
    }

}
