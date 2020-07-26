using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Enemy;

public class SuitMan : Enemy {

    protected override void Attack() {
        animator.SetTrigger("MoneyAttack");
        Invoke("ThrowMoney", attackAnimationDelay);
    }

    private void ThrowMoney() {
        Instantiate(projectile, shotPoint.position, Quaternion.identity);
    }

}
