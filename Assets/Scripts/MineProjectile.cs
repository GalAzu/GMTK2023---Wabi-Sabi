using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineProjectile : Projectile
{
    [SerializeField] private float explosionTime;
    public LayerMask playersLayer;
    [SerializeField] private float explosionRadius;
    [SerializeField] private float explosionDamage;

    private void Start()
    {
        projectileSpeed = 0;
        StartCoroutine(StartProjectileTimer());
    }

    private IEnumerator StartProjectileTimer()
    {
        float counter = 0;
        while (counter < explosionTime)
        {
            counter += Time.deltaTime;
            yield return null;
        }
        Explode();
    }

    private void Explode()
    {
        Collider2D[] targetsToHit = Physics2D.OverlapCircleAll(transform.position, explosionRadius, playersLayer);
        Debug.Log("" + targetsToHit.Length);
        foreach (var target in targetsToHit)
        {
            target.GetComponent<PlayerStats>().OnDamage(explosionDamage);
        }
        AudioManager.instance.PlaySFXFromPool(_AudioStuff.SfxToPlay.Explosion, AudioManager.staticSFXpos);
        Destroy(this.gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerStats>(out PlayerStats player))
        {
            Explode();
        }
    }
}
