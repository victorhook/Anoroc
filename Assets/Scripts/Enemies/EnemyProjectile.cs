using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour {

    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;
    [SerializeField] private float distance;
    private int damage;

    public GameObject destroyEffect;
    public LayerMask whatIsSolid;
    private Vector2 direction;

    void Awake() {
        Invoke("Die", lifeTime);
        damage = 0;
        // gets the direction to the player.
        // this is used so the projectile is always aimed at the player
        Transform playerPos = GameObject.FindWithTag("player").transform;
        direction = playerPos.position - transform.position;
        direction.Normalize();
    }

    void Update() {
        transform.Translate(direction * speed * Time.deltaTime);

        if (damage > 0) {
            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position,
                                                     transform.up, distance,
                                                     whatIsSolid);
            if (hitInfo.collider != null) {
                if (hitInfo.collider.CompareTag("player")) {
                    hitInfo.collider.GetComponent<PlayerController>().TakeDamage(damage);
                }
            Die();
            }
        }

    }

    public void SetDamage(int damage) {
        this.damage = damage;
    }

    void Die() {
        //Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
