using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelEntry : MonoBehaviour {

    public NextLevelMenu nextLevelMenu;

    void OnCollisionEnter2D(Collision2D collider) {
        if (collider.gameObject.name == "Player") {
            PlayerController.SaveStaticVariables();
            ScoreHandler.SaveStaticVariables();
            LevelHandler.SaveStaticVariables();
            nextLevelMenu.LevelComplete();
        }
    }

}
