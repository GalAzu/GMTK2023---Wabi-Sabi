using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 direction = new Vector3(1f, 0f, 0f);

    private float projectileLifeSpan = 5f;
    private float projectileRemaningTime;
    [SerializeField]
    protected float projectileSpeed = 10;

    [SerializeField]
    private bool shootingRight;

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

        if (projectileRemaningTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void ProjectileMovement()
    {
        if (!shootingRight)
        {
            Debug.Log("SHOOT LEFT");
            transform.Translate(-direction * Time.deltaTime * projectileSpeed);
        }
        else
        {
            Debug.Log("SHOOT RIGHT");
            transform.Translate(direction * Time.deltaTime * projectileSpeed);
        }
    }

    public void SetShootingDirection(bool isShootingRight)
    {
        shootingRight = isShootingRight;
    }
}
