using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointItem : MonoBehaviour {

    public int pointsWorth;

    void OnTriggerEnter2D(Collider2D collision) {
        GameObject collider = collision.gameObject;

        if (collider.name == "Player") {
            collider.GetComponent<PlayerController>().GivePoints(pointsWorth);
            Destroy(gameObject);
        }
    }

}
