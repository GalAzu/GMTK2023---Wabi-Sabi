using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezingProjectile : Projectile
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerStats>(out PlayerStats player))
        {
            player.OnDamage(1f);
            player.OnFreeze(2f);
            Destroy(gameObject);
        }
    }
}
