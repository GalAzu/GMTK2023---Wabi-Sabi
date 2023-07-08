using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AbilityCastPoint : MonoBehaviour
{
    private float abilityCastTimerMax = 0.2f;
    private float abilityCastCurrentTime;

    [SerializeField] private GameObject fireballProjectile;
    [SerializeField] private GameObject slowingProjectile;

    private Array abilitiesArray;

    private void Awake()
    {
        abilityCastCurrentTime = abilityCastTimerMax;
        abilitiesArray = Enum.GetValues(typeof(Abilities));
    }

    private void Update()
    {
        AbilityCastTimer();
    }

    private void AbilityCastTimer()
    {
        abilityCastCurrentTime -= Time.deltaTime;

        if (abilityCastCurrentTime <= 0)
        {
            CastAbility((Abilities)abilitiesArray.GetValue(UnityEngine.Random.Range(0, abilitiesArray.Length)));
            abilityCastCurrentTime = abilityCastTimerMax;
        }
    }

    private void CastAbility(Abilities castAbility)
    {
        switch (castAbility)
        {
            case Abilities.Fireball:
                Instantiate(fireballProjectile, transform.position, Quaternion.identity);
                break;
            case Abilities.Slowing:
                Instantiate(slowingProjectile, transform.position, Quaternion.identity);
                break;
        }
    }
}
