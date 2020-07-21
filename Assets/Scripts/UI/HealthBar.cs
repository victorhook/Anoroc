using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using static PlayerController;

public class HealthBar : MonoBehaviour {

    [SerializeField] private Text uiText;
    [SerializeField] private Slider slider;
    [SerializeField] private Transform player;

    private Vector2 moveVector, diffVector;

    void Start() {
        // since the healthbar is positioned a bit away from
        // the player, we need to know take the difference between them
        // into consideration when moving the healthbar
        diffVector = player.position - transform.position;
    }

    void Update() {

        // check if player has moved from us and if so
        // the healthbar moves as well
        moveVector = player.position - transform.position;
        moveVector -= diffVector;
        
        if (moveVector.magnitude > 0) {
            transform.Translate(moveVector);
        }

        // update the UI text
        uiText.text = "HP: " + ( (int) slider.value).ToString();
    }

    /* helper-methods to update the slider */
    public void SetMax(int max) {
        slider.maxValue = max;
    }
    public void Set(int currentHitpoints) {
        slider.value = currentHitpoints;
    }

    public void Decrease(int valueDiff) {
        slider.value -= valueDiff;
    }

    public void Increase(int valueDiff) {
        slider.value += valueDiff;
    }

    public void Reset() {
        slider.value = slider.maxValue;
    }

}


