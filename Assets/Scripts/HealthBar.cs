using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private float maxHealthValue = 100f;

    private Slider healthBar;

    private float healthBarValue;

    private void Awake()
    {
        healthBar = GetComponent<Slider>();
        healthBarValue = maxHealthValue;

    }

    [Button]
    public void HealthBarDamage(float damageAmount)
    {
        healthBarValue -= damageAmount;
        healthBarValue = Mathf.Clamp(healthBarValue, 0f, maxHealthValue);
        healthBar.value = healthBarValue;
    }
}
