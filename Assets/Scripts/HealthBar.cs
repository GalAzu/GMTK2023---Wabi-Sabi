using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private float maxHealthValue = 100f;

    private Slider healthBar;

    private float healthBarValue;

    public void HealthBarDamage(float damageAmount)
    {
        healthBarValue -= damageAmount;
        healthBarValue = Mathf.Clamp(healthBarValue, 0f, maxHealthValue);
        healthBar.value = healthBarValue;
    }
}
