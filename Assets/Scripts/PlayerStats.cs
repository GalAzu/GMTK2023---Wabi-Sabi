using UnityEngine;
using System.Collections;
using System;

public class PlayerStats : MonoBehaviour
{
    public float maxHealth = 100;
    public float curHealth;
    private float initSpeed = 5;
    public float moveSpeed;
    public float jumpForce = 5;
    private bool isBurning;
    private Rigidbody2D rb;

    private float knockbackForce = 1;

    private float stackDamagePercentage;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        moveSpeed = initSpeed;
    }

    public void OnDamage(float damage, Vector2 damageSource)
    {
        curHealth -= damage;

        ApplyKnockback(damageSource);


        if (curHealth <= 0)
        {
            Die();
        }
    }
    public void OnDamage(float damage)
    {
        curHealth -= damage;
        //Update HP UI
        if (curHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        throw new NotImplementedException();
    }

    public void OnSlow(float slowRate)
    {
        moveSpeed *= 0.1f * slowRate;
    }

    public IEnumerator OnFreeze(float freezeTime)
    {
        moveSpeed = 0;
        yield return new WaitForSeconds(freezeTime);
        moveSpeed = initSpeed;
    }

    public IEnumerator OnBurn(float burnTime, float burnDamage)
    {
        float counter = 0;
        isBurning = true;

        while (isBurning && counter < burnTime)
        {
            Debug.Log("BURN DAMAGE EFFECT");
            OnDamage(burnDamage); // Apply burn damage without recoil

            counter += Time.deltaTime;

            yield return null;
        }

        isBurning = false;
        
    }

    private void ApplyRecoil(Vector2 damageSource)
    {
        Vector2 knockbackDirection = -((Vector2)transform.position - damageSource).normalized;
        rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
    }
    private void ApplyKnockback(Vector2 hitDirection)
    {
        float recoilMultiplier = 1f + (stackDamagePercentage / 100f);
        Vector2 recoilForceVector = Vector2.up * recoilMultiplier;
        rb.AddForce(recoilForceVector, ForceMode2D.Impulse);

    }

}
