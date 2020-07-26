using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSanitizer : MonoBehaviour
{
    public GameObject projectile;
    public PlayerController player;
    private Transform shotPoint;
    private float timeBetweenShots;
    public float startTimeBetweenShots;

    void Start() {
        shotPoint = transform.Find("ShotPoint");
    }

    void Update() {

        if (timeBetweenShots >= player.GetShootDelay()) {
            if (Input.GetMouseButtonDown(0)) {
                Instantiate(projectile, shotPoint.position, shotPoint.rotation);
                timeBetweenShots = 0;
            }
        } else {
            timeBetweenShots += Time.deltaTime;
        }
    }

}
