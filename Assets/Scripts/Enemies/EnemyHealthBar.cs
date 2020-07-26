using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using static PlayerController;

public class EnemyHealthBar : MonoBehaviour {

    [SerializeField] private Slider slider;
    [SerializeField] private Transform enemyTransform;

    private Vector2 moveVector, diffVector;

    void Start() {
        // since the healthbar is positioned a bit away from
        // the enemy, we need to know take the difference between them
        // into consideration when moving the healthbar
        //diffVector = enemy.position - transform.position;
    }

    public void SetEnemyTransform(Transform enemy) {
        enemyTransform = enemy;
        diffVector = enemyTransform.position - transform.position;
        print("HEY");
    }

    void Update() {

        return;
        // check if enemy has moved from us and if so
        // the healthbar moves as well
        moveVector = enemyTransform.position - transform.position;
        moveVector -= diffVector;
        
        if (moveVector.magnitude > 0) {
            transform.Translate(moveVector);
        }

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


