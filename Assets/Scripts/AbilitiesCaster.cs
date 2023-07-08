using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;
public class AbilitiesCaster : MonoBehaviour
{
    [SerializeField]
    private float abilityCastInterval;
    [SerializeField]
    private float abilityCastCurrentTime;
    [SerializeField]
    private PlayerMovement playerMovement;
    public Transform abilitiesCastPoint;
    [SerializeField] private Fireball fireballProjectile;
    [SerializeField] private MineProjectile slowingProjectile;
    [SerializeField] private FreezingProjectile freezingProjectile;
    [SerializeField]
    private Array abilitiesArray;
    private bool shootingDirection { get => playerMovement.isFacingRight; }
    [SerializeField] private int IntensityLevel;
    [SerializeField] private int intensityIntervalInSeconds = 20;
    private int maxIntensityLevel = 5;

    private void Awake()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
        abilityCastCurrentTime = abilityCastInterval;
        abilitiesArray = Enum.GetValues(typeof(Abilities));
        IntensityLevel = 1;

    }
    void Start()
    {
        InvokeRepeating("IncreaseIntensityLevel", intensityIntervalInSeconds, intensityIntervalInSeconds);
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
            abilityCastCurrentTime = abilityCastInterval;
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
                MineProjectile slowing = Instantiate(slowingProjectile, abilitiesCastPoint.position, Quaternion.identity);
                slowing.SetShootingDirection(shootingDirection);
                break;
            case Abilities.Freezing:
                FreezingProjectile freezing = Instantiate(freezingProjectile, abilitiesCastPoint.position, Quaternion.identity);
                freezing.SetShootingDirection(shootingDirection);
                break;
        }
    }
    private void IncreaseIntensityLevel()
    {
        if (IntensityLevel < maxIntensityLevel)
        {
            IntensityLevel++;
            IntensityState(IntensityLevel);
        }
    }
    public void IntensityState(int stateNumber)
    {
        switch (IntensityLevel)
        {
            case (1):
                abilityCastInterval = 7;
                //level 1 stats
                break;
            case (2):
                abilityCastInterval = 6;
                //level 2 stats
                break;
            case (3):
                abilityCastInterval = 5;
                //level 3 stats
                break;
            case (4):
                abilityCastInterval = 4;
                //level 4 stats
                break;
            case (5):
                abilityCastInterval = 3;
                //level 5 stats
                break;
        }

    }
}
