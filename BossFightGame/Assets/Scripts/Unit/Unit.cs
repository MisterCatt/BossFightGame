
using UnityEngine;

public abstract class Unit : MonoBehaviour, IDamageable
{
    [Header("Unit class")]
    [Space]
    public Rigidbody2D RigidBody;
    public Collider2D ColliderBox, TriggerBox;
    protected int Health = 100;

    public void TakeDamage(int damage)
    {
        Health -= damage;

        if(Health <= 0)
            Death();
    }

    public virtual void Death()
    {
        gameObject.SetActive(false);
    }
}
