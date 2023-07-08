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
    private Rigidbody2D rb;

    private float stackDamagePercentage;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        moveSpeed = initSpeed;
    }

    public void OnDamage(float damage)
    {
        curHealth -= damage;
        ApplyRecoil();
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
            // OnDamage(burnDamage, Vector2.zero); // Apply burn damage without recoil

            counter += Time.deltaTime;
            yield return null;
        }

        isBurning = false;
    }

    private void ApplyRecoil(Vector2 hitDirection)
    {
        float recoilMultiplier = 1f + (stackDamagePercentage / 100f);
        Vector2 recoilForceVector = -hitDirection * recoilMultiplier;
        rb.AddForce(recoilForceVector, ForceMode2D.Impulse);
    }
    private void ApplyRecoil()
    {
        float recoilMultiplier = 1f + (stackDamagePercentage / 100f);
        Vector2 recoilForceVector = Vector2.up * recoilMultiplier;
        rb.AddForce(recoilForceVector, ForceMode2D.Impulse);

    }
}
