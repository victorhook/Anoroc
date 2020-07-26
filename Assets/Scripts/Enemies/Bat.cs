using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Enemy;

public class Bat : Enemy {

    protected override void Attack() {
        //animator.SetTrigger("SpitAttack");
        Invoke("Spit", attackAnimationDelay);
    }

    private void Spit() {
        Instantiate(projectile, shotPoint.position, Quaternion.identity);
    }

}
