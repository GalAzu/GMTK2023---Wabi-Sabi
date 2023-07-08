using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Projectile
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerStats>(out PlayerStats player))
        {
            player.OnDamage(5f);
            StartCoroutine(player.OnBurn(100f, 5f));
            Destroy(gameObject);
        }
    }
}
