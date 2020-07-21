using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Enemy;

public class ProjectileHandSanitizer : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public float distance;
    public int damage;

    public GameObject destroyEffect;

    public LayerMask whatIsSolid;

    void Start() {
        Invoke("Die", lifeTime);
    }

    void Update() {
        transform.Translate(Vector2.up * speed * Time.deltaTime);

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, 
                                        transform.up, distance, whatIsSolid);
        if (hitInfo.collider != null) {
            if (hitInfo.collider.CompareTag("Enemy")) {
                print("hit");

                hitInfo.collider.GetComponent<Enemy>().TakeDamage(damage);
            }
            Die();
        }
    }

    void Die() {
        //Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
