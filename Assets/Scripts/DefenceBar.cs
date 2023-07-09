using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefenceBar : MonoBehaviour
{
    private Slider defenseBar;

    [SerializeField] private float maxDefenseValue = 100f;
    public float MaxDefenceValue { get => maxDefenseValue; }
    [SerializeField] private float defenseBarIncreasePerSecond;

    private float defenseBarValue;
    public float DefenceBarValue { get => defenseBarValue; }

    private float defenseBarTimer = 0f;

    private void Awake()
    {
        defenseBar = GetComponent<Slider>();
    }

    private void Start()
    {
        defenseBarValue = maxDefenseValue;
        defenseBar.maxValue = maxDefenseValue;
        defenseBar.value = defenseBarValue;
    }

    private void Update()
    {
        DefenseBarTimer();
    }

    private void DefenseBarTimer()
    {
        defenseBarTimer += Time.deltaTime;

        if (defenseBarTimer >= 1f)
        {
            IncreaseDefenseBar(defenseBarIncreasePerSecond);
            defenseBarTimer = 0f;
        }
    }

    private void IncreaseDefenseBar(float amount)
    {
        defenseBarValue += amount;
        defenseBarValue = Mathf.Clamp(defenseBarValue, 0f, maxDefenseValue);
        defenseBar.value = defenseBarValue;
    }

    public void DefenseBarDamage(float damageAmount)
    {
        defenseBarValue -= damageAmount;
        defenseBarValue = Mathf.Clamp(defenseBarValue, 0f, maxDefenseValue);
        defenseBar.value = defenseBarValue;
    }
}
