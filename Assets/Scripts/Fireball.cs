using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Projectile
{
    private void Start()
    {
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerStats>(out PlayerStats player))
        {
            player.StartBurnCorutine();
            AudioManager.instance.PlaySFXFromPool(_AudioStuff.SfxToPlay.FireHit, AudioManager.staticSFXpos);
        }
        Destroy(gameObject);
    }
}
