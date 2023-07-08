using UnityEngine;
using System.Collections;
public class PlayerStats : MonoBehaviour
{
    public float maxHealth = 100;
    public float curHealth;
    private float initSpeed = 5;
    public float moveSpeed;
    public float jumpForce = 5;
    private bool isBurning;

    void Awake()
    {
        moveSpeed = initSpeed;
    }
    public void OnDamage(float damage)
    {
        curHealth -= damage;
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
            OnDamage(burnDamage);

            counter += Time.deltaTime;
            yield return null;
        }

        isBurning = false;
    }
}
