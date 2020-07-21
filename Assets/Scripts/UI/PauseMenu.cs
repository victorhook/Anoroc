using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public static bool gameIsPaused;

    public GameObject pauseMenuUI;

    void Awake()  {
        gameIsPaused = false;
        Resume();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {

            if (gameIsPaused) {
                Resume();
            } else {
                Pause();
            }
        }
    }

    public void Resume() {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    /* opens the pause-UI and freezes game time */
    public void Pause() {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }


}
