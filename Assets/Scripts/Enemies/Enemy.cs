using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {

    public GameObject projectile;
    public GameObject destroyEffect;
    public GameObject itemDroppedOnDeath;
    public Transform shotPoint;

    protected float attackAnimationDelay;
    public float attackDelay;
    public int xpWorth;

    [SerializeField] protected int damage;
    [SerializeField] protected float range;

    [SerializeField] protected int hitpoints;
    [SerializeField] protected float speed;

    protected PlayerController player;
    protected Animator animator;

    private float dirX;
    private bool isMoving;
    private bool attackingPlayer;
    private float secsSinceLastAttack;
    private Transform playerPos;
    private Rigidbody2D rb;

    public LayerMask hitMask;
    

    void Start() {
        animator = GetComponent<Animator>(); 
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        playerPos = player.GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();

        range = 10;
        attackDelay = 2;
        speed = 1;

    }

    void Update() {
        if (Input.GetMouseButtonDown(1)) {
            Attack();
        }
        
        // Detect when the player is within attack-range
        Vector2 directionToPlayer = playerPos.position - transform.position;
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, directionToPlayer,
                                                 range, hitMask);
        dirX = (directionToPlayer.x < 0) ? -1 : 1;

        if (hitInfo) {
            // If the player is within range, we start chasing him!
            if (hitInfo.collider.name == "Player") {
                attackingPlayer = true;
            }
        } else {
            attackingPlayer = false;
        }

        
        if (attackingPlayer) {
            isMoving = true;

            // Checking if it's time for a new attack or if we should wait some more.
            secsSinceLastAttack += Time.deltaTime;
            if (secsSinceLastAttack >= attackDelay) {
                Attack();
                secsSinceLastAttack = 0;
            }
        }

        animator.SetBool("isMoving", isMoving);
    }

    void FixedUpdate() {
        if (isMoving) {
            rb.velocity = new Vector2(dirX * speed, rb.velocity.y);
        }
        else {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    protected void Die() {
        /* When dying we play a dying-animation, give xp to the player
            and spawn an item, dropped on death  */
        PlayDieAnimation();
        if (destroyEffect) {
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
        }
        player.GiveXp(xpWorth);
        Instantiate(itemDroppedOnDeath, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    protected void PlayDieAnimation() {
        animator.SetTrigger("Dead");
    }

    public void TakeDamage(int damage) {
        hitpoints -= damage;
        if (hitpoints <= 0) {
            Die();
        }
    }

    protected abstract void Attack();    
}
