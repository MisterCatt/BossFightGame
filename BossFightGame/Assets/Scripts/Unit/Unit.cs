
using System;
using UnityEngine;

public abstract class Unit : MonoBehaviour, IDamageable, IHealable
{
    [Header("Unit class")]
    [Space]
    public Rigidbody2D RigidBody;
    public Collider2D ColliderBox, TriggerBox;
    [SerializeField]
    protected int Health = 100;

    public event Action OnUnitDeath;
    public event Action<int> OnUnitTakeDamage, OnUnitHeal;
    public int GetCurrentHealth() => Health;

    public virtual void TakeDamage(int damage)
    {
        Health -= damage;

        OnUnitTakeDamage?.Invoke(-damage);

        if (Health <= 0)
            Death();
    }

    public virtual void Death()
    {
        OnUnitDeath?.Invoke();
        gameObject.SetActive(false);
    }

    public virtual void HealUnit(int healAmmount)
    {
        if (Health >= 100) return;

        if (healAmmount + Health > 100)
        {
            Health += healAmmount - Health;
            OnUnitHeal?.Invoke(healAmmount - Health);
        }
        else
        {
            Health += healAmmount;
            OnUnitHeal?.Invoke(healAmmount);
        }

        
    }
}
