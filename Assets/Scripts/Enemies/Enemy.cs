using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {

    [SerializeField] protected GameObject projectile;
    [SerializeField] protected Transform shotPoint;

    [SerializeField] protected int damage;
    [SerializeField] protected int range;

    [SerializeField] protected int hitpoints;
    [SerializeField] protected float speed;
    
    [SerializeField] protected float attackAnimationDelay;

    protected PlayerController player;
    protected Animator animator;
    protected int isMoving;

    public GameObject destroyEffect;
    public int xpWorth;
    public GameObject itemDroppedOnDeath;
    

    void Start() {
        animator = GetComponent<Animator>(); 
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update() {
        if (Input.GetMouseButtonDown(1)) {
            Attack();
        }
    }

    protected void Die() {
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

    /* initiates the attack and then calls the 
        specific attack method of the child */
    protected void _Attack() {
        Attack();
    }

    protected abstract void Attack();    

}
