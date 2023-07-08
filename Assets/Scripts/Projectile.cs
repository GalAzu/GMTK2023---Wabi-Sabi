using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 direction = new Vector3(1f, 0f, 0f);

    private float projectileLifeSpan = 5f;
    private float projectileRemaningTime;

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
        transform.Translate(direction * Time.deltaTime * 10);
    }
}
