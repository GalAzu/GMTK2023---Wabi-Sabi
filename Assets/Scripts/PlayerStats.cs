using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private PlayerID playerID;

    public float maxHealth = 100;
    public float curHealth;
    [SerializeField] private float initSpeed;
    public float moveSpeed { set; get; }
    public float jumpForce;
    private Rigidbody2D rb;

    private float knockbackForce = 1;

    private float stackDamagePercentage;

    private PlayerDefence playerDefence;

    void Awake()
    {
        playerDefence = GetComponent<PlayerDefence>();
        rb = GetComponent<Rigidbody2D>();
        moveSpeed = initSpeed;
    }
    public void OnDamage(float damage, Vector2 damageSource)
    {
        if (playerDefence.Defend()) return;

        curHealth -= damage;

        ApplyKnockback(damageSource);

        if (curHealth <= 0)
        {
            Death();
        }
    }
    public void OnDamage(float damage)
    {
        if (playerDefence.Defend()) return;


        curHealth -= damage;

        switch (playerID)
        {
            case PlayerID.PlayerOne:
                UIManager.Instance.playerOneHealthBar.HealthBarDamage(damage);
                break;
            case PlayerID.PlayerTwo:
                UIManager.Instance.playerTwoHealthBar.HealthBarDamage(damage);
                break;
        }

        if (curHealth <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        Debug.Log("DEATH IS UPON US");
        GameManager.Instance.Lose();
    }
    private void ApplyKnockback(Vector2 damageSource)
    {
        Vector2 knockbackDirection = -((Vector2)transform.position - damageSource).normalized;
        rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
    }
    #region AbilitiesAndStatusEffects
    public enum StatusEffectType
    {
        None,
        Burn,
        Freeze
    }
    public List<StatusEffectType> statusEffects;
    private void AddStatusEffect(StatusEffectType _statusEffect)
    {
        statusEffects.Add(_statusEffect);
    }
    private void RemoveStatusEffect(StatusEffectType _statusEffect)
    {
        statusEffects.Remove(_statusEffect);
    }
    public IEnumerator OnFreeze(float freezeTime, float freezeDamage)
    {
        float freezeCounter = 0;
        float innerFreezeCounter = 2;
        AddStatusEffect(StatusEffectType.Freeze);
        while (freezeCounter < freezeTime)
        {
            moveSpeed = 0;
            innerFreezeCounter -= Time.deltaTime;
            if (innerFreezeCounter <= 0)
            {
                OnDamage(freezeDamage);
                innerFreezeCounter = 2;
            }
            freezeCounter += Time.deltaTime;
            yield return null;
            RemoveStatusEffect(StatusEffectType.Freeze);
            moveSpeed = initSpeed;
        }
    }
    public IEnumerator OnBurn(float burnTime, float burnDamage)
    {
        float burnCounter = 0;
        float innerBurnCounter = 2;
        AddStatusEffect(StatusEffectType.Burn);
        while (burnCounter < burnTime)
        {
            innerBurnCounter -= Time.deltaTime;
            if (innerBurnCounter <= 0)
            {
                OnDamage(burnDamage);
                innerBurnCounter = 2;
            }
            burnCounter += Time.deltaTime;
            yield return null;
        }
        RemoveStatusEffect(StatusEffectType.Burn);
    }
    public void StartBurnCorutine()
    {
        if (playerDefence.Defend()) return;

        if (!statusEffects.Contains(StatusEffectType.Burn))
            StartCoroutine(OnBurn(10, 6));
    }
    public void StartFreezeCorutine()
    {
        if (playerDefence.Defend()) return;

        if (!statusEffects.Contains(StatusEffectType.Freeze))
            StartCoroutine(OnFreeze(10, 2));
    }
    #endregion

}
