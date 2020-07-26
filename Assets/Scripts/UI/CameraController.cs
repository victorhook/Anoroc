using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public PlayerController playerController;
    public Transform player;
    public Vector3 offset;

    public float dirOffset, dirX;
    public int dirChangeIterations, currIteration;

    private bool switching;
    private bool directionRight;
  
    

    void Update () {
        dirX = player.position.x + offset.x + dirOffset;
        transform.position = new Vector3 (dirX, 0, offset.z);
    }


}
