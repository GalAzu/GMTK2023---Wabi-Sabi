using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 direction = new Vector3(1f, 0f, 0f);

    private float projectileLifeSpan = 5f;
    private float projectileRemaningTime;

    private bool shootingRight = true;

    private void Awake()
    {
        projectileRemaningTime = projectileLifeSpan;
    }

    private void Update()
    {
        ProjectileMovement();
        DestroyAfterSeconds();
    }

    private void DestroyAfterSeconds()
    {
        projectileRemaningTime -= Time.deltaTime;

        if(projectileRemaningTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void ProjectileMovement()
    {
        if (!shootingRight)
        {
            transform.Translate(-direction * Time.deltaTime * 10);
        }
        else
        {
            transform.Translate(direction * Time.deltaTime * 10);
        }
    }

    public void SetShootingDirection(bool isShootingRight)
    {
        shootingRight = isShootingRight;
    }
}
