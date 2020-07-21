using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Enemy;

public class Man : Enemy
{

    private float timeBetweenShots;
    public float startTimeBetweenShots;

    void Awake() {
        hitpoints = 10;
        damage = 10;
        range = 10;
        speed = 10;        
        attackAnimationDelay = .7f;
    }

    protected override void Attack() {
        animator.SetTrigger("SneezeAttack");
        Invoke("Sneeze", attackAnimationDelay);
    }

    private void Sneeze() {
        Instantiate(projectile, shotPoint.position, Quaternion.identity);
    }


}
