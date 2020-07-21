using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSanitizer : MonoBehaviour
{
    public GameObject projectile;
    private Transform shotPoint;
    private float timeBetweenShots;
    public float startTimeBetweenShots;

    void Start() {
        shotPoint = transform.Find("ShotPoint");
    }

    void Update()
    {

        if (timeBetweenShots <= 0) {
            if (Input.GetMouseButtonDown(0)) {
                Instantiate(projectile, shotPoint.position, shotPoint.rotation);
                timeBetweenShots = startTimeBetweenShots;
            }
        } else {
            timeBetweenShots -= Time.deltaTime;
        }

    }

}
