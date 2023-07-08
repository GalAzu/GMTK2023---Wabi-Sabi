using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefenceBar : MonoBehaviour
{
    private Slider defenseBar;

    [SerializeField] private float defenceBarIncreasePerSecond;

    private float defenseBarTimer = 0f;

    private void Awake()
    {
        defenseBar = GetComponent<Slider>();
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
            defenseBar.value += defenceBarIncreasePerSecond;
            defenseBarTimer -= defenseBarTimer;
        }
    }

    public void DefenseBarDamage(float damageAmount)
    {
        defenseBar.value -= damageAmount;
    }
}
