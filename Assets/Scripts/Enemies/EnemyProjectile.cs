using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyProjectile : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected float lifeTime;
    [SerializeField] protected float distance;
    [SerializeField] protected int damage;

    [SerializeField] protected  GameObject destroyEffect;
    [SerializeField] protected  LayerMask whatIsSolid;
    private Vector2 direction;

    void Awake() {
        Invoke("Die", lifeTime);

        // gets the direction to the player.
        // this is used so the projectile is always aimed at the player
        Transform playerPos = GameObject.FindWithTag("player").transform;
        direction = playerPos.position - transform.position;
        direction.Normalize();
    }

    void Update() {
        transform.Translate(direction * speed * Time.deltaTime);

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, 
                                        transform.up, distance, whatIsSolid);

        if (hitInfo.collider != null) {
            if (hitInfo.collider.CompareTag("player")) {
                hitInfo.collider.GetComponent<PlayerController>().TakeDamage(damage);
            }
            Die();
        }
        
    }

    void Die() {
        //Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
