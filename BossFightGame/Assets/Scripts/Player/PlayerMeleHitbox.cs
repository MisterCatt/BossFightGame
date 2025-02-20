using System.Collections;
using UnityEngine;

public class PlayerMeleHitbox : MonoBehaviour
{
    [SerializeField] Player player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();

        if (damageable == null) return;

        damageable.TakeDamage(player.PlayerClass.GetMeleDamage());
    }
}
