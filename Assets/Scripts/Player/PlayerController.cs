﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

using static LevelHandler;
using static ScoreHandler;
using static HealthBar;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D rb;
    private BoxCollider2D box2d;
    private Animator animator;

    // movement & utilitiy variables
    public LayerMask whatIsGround;

    private Transform armTransform;
    private Transform foot1, foot2, foot3;

    private float dirX;
    private float armOffset;
    private int weaponEquipped;
    private int jumps;

    // the different player skills
    private static float speed, jumpForce, shootDelay;
    private static int hitpoints, jumpsAllowed;
    private static int currHitpoints;

    // flags for movement
    private bool isCrouching;
    private bool isMoving;
    private bool isGrounded;
    public bool facingRight;

    public LevelHandler levelHandler;
    public ScoreHandler scoreHandler;
    public HealthBar healthbar;
    public DieMenu dieMenu;

    [SerializeField] private Weapon weapon;

    private void Cheat() {
        hitpoints = 100;
        jumpForce = 40f;
        jumpsAllowed = 3;
        speed = 12f;
        shootDelay = .1f;
    }

    public static void SaveStaticVariables() {
        PlayerStats.hitpoints = hitpoints;
        PlayerStats.jumpForce = jumpForce;
        PlayerStats.jumpsAllowed = jumpsAllowed;
        PlayerStats.speed = speed;
        PlayerStats.shootDelay = shootDelay;
    }

    public float GetShootDelay() {
        return shootDelay;
    }

    private void InitSkills() {
        // Load all the static skills.
        hitpoints = PlayerStats.hitpoints;
        jumpForce = PlayerStats.jumpForce;
        jumpsAllowed = PlayerStats.jumpsAllowed;
        speed = PlayerStats.speed;
        shootDelay = PlayerStats.shootDelay;

        // Set the rest of the skills to correct starting values.
        weaponEquipped = 0;
        jumps = 0;
        currHitpoints = hitpoints;

        isGrounded = true;
        facingRight = true;

        // Used for rotating the arm smoothly.
        armOffset = 90;
    }

    private void FindObjectReferences() {
        rb = GetComponent<Rigidbody2D>();
        box2d = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        armTransform = transform.Find("RightArm");
        foot1 = transform.Find("Foot1");
        foot2 = transform.Find("Foot2");
        foot3 = transform.Find("Foot3");
    }


    void Start() {
        FindObjectReferences();
        InitSkills();
        healthbar.SetMax(hitpoints);
        healthbar.Set(currHitpoints);
        //Cheat();
        //EquipWeapon(1);
    }


    void OnCollisionEnter2D(Collision2D collider) {
        Transform colliderTransform = collider.transform;

        Transform[] feet = {foot1, foot2, foot3};
        foreach(Transform foot in feet) {
            if (TouchedGround(foot)) {
                isGrounded = true;
                animator.SetBool("isJumping", false);
                jumps = 0;
                return;
            }
        }
    }

    private bool TouchedGround(Transform foot) {
        RaycastHit2D hitInfo = Physics2D.Raycast(foot.position, Vector3.down,
                                                .3f, whatIsGround);
        if (hitInfo)
        {
            return hitInfo.collider.tag == "ground" ||
                    hitInfo.collider.tag == "Enemy";
        }
        return false;
    }


    public void PickUp(Item wep) {
        print(wep);
    }

    public void TakeDamage(int damage) {
        currHitpoints -= damage;
        healthbar.Decrease(damage);
        if (currHitpoints <= 0) {
            Die();
        }
    }

    private void Die() {
        dieMenu.PlayerDied();
    }


    void Update() {

        if (!PauseMenu.gameIsPaused) {

            Debug.DrawRay(foot1.position, Vector3.down*.1f, Color.green);
            Debug.DrawRay(foot2.position, Vector3.down*.1f, Color.green);


            // read player input
            dirX = Input.GetAxisRaw("Horizontal");
            isMoving = dirX != 0;

            if (Input.GetKeyDown(KeyCode.C)) {
                isCrouching = !isCrouching;
            }

            animator.SetBool("isMoving", isMoving);
            animator.SetBool("isCrouching", isCrouching);

            if (facingRight && dirX < 0) {
                Flip();
            } else if (!facingRight && dirX > 0) {
                Flip();
            }

            // check for jump and if we're allowed to doublejump, we can do that
            if (Input.GetButtonDown("Jump")) {
                if (isGrounded || jumpsAllowed >= 1 && jumps < jumpsAllowed) {
                    rb.AddForce(new Vector2(rb.velocity.x, jumpForce), ForceMode2D.Impulse);
                    isGrounded = false;
                    animator.SetTrigger("takeOff");
                    animator.SetBool("isJumping", true);
                    jumps++;
                }
            }


            /* --- ARM ROTATION --- */

            // calculate the distance vector between mouse and weapon
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - armTransform.position;
            // get the angle between y and x axis
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            // rotate the weapon
            armTransform.rotation = Quaternion.Euler(0f, 0f, rotZ + armOffset);

            healthbar.SetMax(hitpoints);

        }
    }


    private void EquipWeapon(int weapon) {
        weaponEquipped = weapon;
        animator.SetInteger("weapon", weaponEquipped);
        animator.SetBool("newEeapon", true);
    }


    private void Flip() {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        facingRight = !facingRight;
    }

    void FixedUpdate() {

        if (isMoving) {
            rb.velocity = new Vector2(dirX * speed, rb.velocity.y);
        }
        else {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    public void GivePoints(int points) {
        scoreHandler.Increase(points);
    }
    public void GiveXp(int xp) {
        levelHandler.Increase(xp);
    }
    public void GiveWeapon(Weapon weapon) {

    }


    public void IncreaseSpeed(float speedIncrease) {
        speed += speedIncrease;
    }

    public void IncreaseHp(int hpIncrease) {
        hitpoints += hpIncrease;
        healthbar.SetMax(hitpoints);
        healthbar.Increase(hpIncrease);
    }

    public void IncreaseShootSpeed(float speedIncrease) {
        shootDelay -= speedIncrease;
    }

    public void IncreaseJumpHeight(float jumpIncrease) {
        jumpForce += jumpIncrease;
    }

    public void IncreaseDoubleJump() {
        jumpsAllowed++;
    }



}

