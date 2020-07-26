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

    // movement & utilitiy variables
    public LayerMask whatIsGround;
    
    private Transform armTransform;
    private Transform foot1, foot2;

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

    [SerializeField] private LevelHandler levelHandler;
    [SerializeField] private ScoreHandler scoreHandler;
    [SerializeField] private HealthBar healthbar;

    [SerializeField] private Weapon weapon;


    public static void SaveStaticVariables() {
        PlayerStats.Hitpoints = hitpoints;
        PlayerStats.JumpForce = jumpForce;
        PlayerStats.JumpsAllowed = jumpsAllowed;
        PlayerStats.Speed = speed;
        PlayerStats.ShootDelay = shootDelay;
    }

    public float GetShootDelay() {
        return shootDelay;
    }

    private void InitSkills() {
        // Load all the static skills.
        hitpoints = PlayerStats.Hitpoints;
        jumpForce = PlayerStats.JumpForce;
        jumpsAllowed = PlayerStats.JumpsAllowed;
        speed = PlayerStats.Speed;
        shootDelay = PlayerStats.ShootDelay;

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
    }


    void Start() {
        FindObjectReferences();
        InitSkills();
        healthbar.SetMax(hitpoints);
        healthbar.Set(currHitpoints);

        //EquipWeapon(1);
    }


    void OnCollisionEnter2D(Collision2D collider) {
        Transform colliderTransform = collider.transform;

        RaycastHit2D hitInfo1 = Physics2D.Raycast(foot1.position, Vector3.down,
                                                .3f, whatIsGround);
        RaycastHit2D hitInfo2 = Physics2D.Raycast(foot2.position, Vector3.down,
                                                .3f, whatIsGround);
        
        if (hitInfo1) {
            if (hitInfo1.collider.tag == "ground" ||
                hitInfo1.collider.tag == "Enemy") {
                isGrounded = true;
                animator.SetBool("isJumping", false);
                jumps = 0;
            }
        }   
        else if (hitInfo2) {
            if (hitInfo2.collider.tag == "ground" ||
                hitInfo2.collider.tag == "Enemy") {
            isGrounded = true;
            animator.SetBool("isJumping", false);
            jumps = 0;
            } 
        }

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
        print("YOU LOOSE!!!");
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

