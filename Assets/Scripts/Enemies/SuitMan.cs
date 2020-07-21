using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Enemy;

public class SuitMan : Enemy
{
    private float timeBetweenShots;
    public float startTimeBetweenShots;

    void Awake() {
        damage = 10;
        range = 10;
        speed = 10;        
        attackAnimationDelay = .7f;
    }


    protected override void Attack() {
        animator.SetTrigger("MoneyAttack");
        Invoke("ThrowMoney", attackAnimationDelay);
    }

    private void ThrowMoney() {
        Instantiate(projectile, shotPoint.position, Quaternion.identity);
    }

}
