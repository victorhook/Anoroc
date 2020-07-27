using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public PlayerController playerController;
    public Transform player;
    public Vector3 offset;

    private float dirOffset, dirX, dirY;
    public int dirChangeIterations, currIteration;

    private bool switching;
    private bool directionRight;
    public bool canMoveInY;
  

    void Update () {
        dirX = player.position.x + offset.x + dirOffset;
        if (canMoveInY) {
            dirY = player.position.y + offset.y;
        } else {
            dirY = 0;
        }

        transform.position = new Vector3 (dirX, dirY, offset.z);
    }


}
