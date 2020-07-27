using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Enemy;

public class Man : Enemy {

    void Start() {
        facingRight = true;
    }

    protected override void Attack() {
        animator.SetTrigger("SneezeAttack");
        Invoke("SendProjectile", attackAnimationDelay);
    }

}
