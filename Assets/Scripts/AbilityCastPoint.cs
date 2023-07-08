using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AbilityCastPoint : MonoBehaviour
{
    private float abilityCastTimerMax = 0.2f;
    private float abilityCastCurrentTime;
    private PlayerMovement playerMovement;

    [SerializeField] private GameObject fireballProjectile;
    [SerializeField] private GameObject slowingProjectile;
    [SerializeField] private GameObject freezingProjectile;

    private Array abilitiesArray;

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
            CastAbility((Abilities)abilitiesArray.GetValue(UnityEngine.Random.Range(0, 1/*, abilitiesArray.Length*/)));
            abilityCastCurrentTime = abilityCastTimerMax;
        }
    }

    private void CastAbility(Abilities castAbility)
    {
        switch (castAbility)
        {
            case Abilities.Fireball:
                GameObject fireball = Instantiate(fireballProjectile, transform.position, Quaternion.identity);
                fireball.GetComponent<Projectile>().SetShootingDirection(playerMovement.IsFacingRight);
                break;
            case Abilities.Slowing:
                GameObject slowing = Instantiate(slowingProjectile, transform.position, Quaternion.identity);
                slowing.GetComponent<Projectile>().SetShootingDirection(playerMovement.IsFacingRight);
                break;
            case Abilities.Freezing:
                GameObject freezing = Instantiate(freezingProjectile, transform.position, Quaternion.identity);
                freezing.GetComponent<Projectile>().SetShootingDirection(playerMovement.IsFacingRight);
                break;
        }
    }
}
