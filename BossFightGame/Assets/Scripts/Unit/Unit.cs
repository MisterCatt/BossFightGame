
using UnityEngine;

public abstract class Unit : MonoBehaviour, IDamageable
{
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
