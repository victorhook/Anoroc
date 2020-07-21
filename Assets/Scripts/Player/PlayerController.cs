using System.Collections;
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
    private Transform armTransform;

    // movement & utilitiy variables
    private float dirX;
    private float armOffset;
    private int weaponEquipped;
    private int jumps;

    // the different player skills
    [SerializeField] private float speed, jumpForce;
    [SerializeField] private int hitpoints, shootDamage, jumpsAllowed;

    // flags for movement    
    private bool isJumping;
    private bool isMoving;
    private bool isGrounded;
    private bool isShooting;
    private bool facingRight;

    [SerializeField] private LevelHandler levelHandler;
    [SerializeField] private ScoreHandler scoreHandler;
    

    // Hitpoints related
    [SerializeField] private int currHitpoints;
    [SerializeField] private HealthBar healthbar;


    [SerializeField] private Weapon weapon;

    
    void Start() {
     
        rb = GetComponent<Rigidbody2D>();
        box2d = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();

        weaponEquipped = 0;

        // set default skill values
        speed = 10f;
        jumpForce = 25f;
        hitpoints = 100;
        shootDamage = 10;
        jumpsAllowed = 1;

        currHitpoints  = hitpoints;

        isGrounded = true;
        facingRight = true;

        // used to rotate the arm towards mouse position
        armTransform = transform.Find("RightArm");
        armOffset = 90;

        healthbar.SetMax(hitpoints);
        healthbar.Set(currHitpoints);

        jumps = 0;

        //EquipWeapon(1);
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.name == "floor") {
            isGrounded = true;
            isJumping = false;
            animator.SetBool("isJumping", false);
            jumps = 0;
        }
    }

    public void PickUp(Item wep) {
        print(wep);
    }

    public void TakeDamage(int damage) {
        print(damage);
        currHitpoints -= damage;
        healthbar.Decrease(damage);
        if (currHitpoints <= 0) {
            Die();
        }
    }

    private void Die() {
        print("YOU LOOSE!!!");
    }


    void Update() {

        if (!PauseMenu.gameIsPaused) {

            // read player input
            dirX = Input.GetAxisRaw("Horizontal");
            isMoving = dirX != 0;

            animator.SetBool("isMoving", isMoving);

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

    public void IncreaseDamage(int damageIncrease) {
        shootDamage += damageIncrease;
    }

    public void IncreaseJumpHeight(float jumpIncrease) {
        jumpForce += jumpIncrease;
    }

    public void IncreaseDoubleJump() {
        jumpsAllowed++;
    }

    

}

