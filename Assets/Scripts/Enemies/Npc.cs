using UnityEngine;

public abstract class Npc : MonoBehaviour {
    
    [SerializeField] protected int hitpoints;
    [SerializeField] protected float speed;
    [SerializeField] protected GameObject destroyEffect;

    protected Animator animator;

    public void TakeDamage(int damage) {
        hitpoints -= damage;
        if (hitpoints <= 0) {
            Die();
        }
    }

    protected void Die() {
        PlayDieAnimation();
        if (destroyEffect) {
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    protected void PlayDieAnimation() {
        animator.SetTrigger("Dead");
    }


}