using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowingProjectile : Projectile
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerStats>(out PlayerStats player))
        {
            player.OnDamage(3f);
            player.OnSlow(5f);
            Destroy(gameObject);
        }
    }
}
