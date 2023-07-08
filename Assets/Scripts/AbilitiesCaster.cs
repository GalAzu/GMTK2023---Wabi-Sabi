using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;
public class AbilitiesCaster : MonoBehaviour
{
    [SerializeField]
    private float abilityCastTimerMax;
    [SerializeField]
    private float abilityCastCurrentTime;
    [SerializeField]
    private PlayerMovement playerMovement;
    public Transform abilitiesCastPoint;

    [SerializeField] private Fireball fireballProjectile;
    [SerializeField] private SlowingProjectile slowingProjectile;
    [SerializeField] private FreezingProjectile freezingProjectile;
    private Array abilitiesArray;
    [ShowInInspector]
    private bool shootingDirection { get => playerMovement.isFacingRight; }

    private void Awake()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
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
            abilityCastCurrentTime = abilityCastTimerMax;
            CastAbility((Abilities)abilitiesArray.GetValue(UnityEngine.Random.Range(0, 1/*, abilitiesArray.Length*/)));
            abilityCastCurrentTime = abilityCastTimerMax;
        }
    }
    [Button]
    private void CastAbility(Abilities castAbility)
    {
        switch (castAbility)
        {
            case Abilities.Fireball:
                Fireball fireball = Instantiate(fireballProjectile, abilitiesCastPoint.position, Quaternion.identity);
                fireball.SetShootingDirection(shootingDirection);
                break;
            case Abilities.Slowing:
                SlowingProjectile slowing = Instantiate(slowingProjectile, abilitiesCastPoint.position, Quaternion.identity);
                slowing.SetShootingDirection(shootingDirection);
                break;
            case Abilities.Freezing:
                FreezingProjectile freezing = Instantiate(freezingProjectile, abilitiesCastPoint.position, Quaternion.identity);
                freezing.SetShootingDirection(shootingDirection);
                break;
        }
    }
}
