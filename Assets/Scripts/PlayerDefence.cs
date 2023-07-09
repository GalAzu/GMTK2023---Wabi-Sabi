using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDefence : MonoBehaviour
{
    [SerializeField] private DefenceBar defenceBar;

    private bool canDefend = true;

    private void Update()
    {
        if(defenceBar.DefenceBarValue >= defenceBar.MaxDefenceValue)
        {
            canDefend = true;
        }
    }

    public bool Defend()
    {
        if (canDefend)
        {
            canDefend = false;
            defenceBar.DefenseBarDamage(defenceBar.MaxDefenceValue);
            return true;
        }

        return false;
    }
}
