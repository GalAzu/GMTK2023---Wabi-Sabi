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

    public float burnCounter;
    public float innerBurnCounter;
    public enum StatusEffectType
    {
        None,
        Burn,
        Freeze
    }

    public StatusEffectType statusEffect;


    public Action<Projectile> statusEffectActivation;

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
        Debug.Log("DEATH IS UPON US");
    }

    public void OnSlow(float slowRate)
    {
        moveSpeed *= 0.1f * slowRate;
    }

    public IEnumerator OnFreeze(float freezeTime)
    {
        statusEffect = StatusEffectType.Freeze;
        moveSpeed = 0;
        yield return new WaitForSeconds(freezeTime);
        moveSpeed = initSpeed;
    }
    public IEnumerator OnBurn(float burnTime, float burnDamage)
    {
        float burnCounter = 0;
        float innerBurnCounter = 2;
        statusEffect = StatusEffectType.Burn;
        while (burnCounter < burnTime) // Keep running until the burnTime is reached
        {
            Debug.Log("COROUTINE STILL RUNNING");
            innerBurnCounter -= Time.deltaTime;
            if (innerBurnCounter <= 0)
            {
                OnDamage(burnDamage);
                Debug.Log("BURN DAMAGE");
                innerBurnCounter = 2;
            }
            burnCounter += Time.deltaTime;
            yield return null;
        }
        statusEffect = StatusEffectType.None;
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
    public void StartBurnCorutine() => StartCoroutine(OnBurn(3, 5));
    public void StartFreezeCorutine() => StartCoroutine(OnFreeze(5));

}
