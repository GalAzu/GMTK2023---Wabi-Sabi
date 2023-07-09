using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorShower : MonoBehaviour
{
    private Transform[] meteorArray;

    private float meteorsIntensityTimerMax = 3f;
    private float meteorsIntensityTimer = 0f;

    private int intensityLevel = 1;

    private void Awake()
    {
        meteorArray = GetComponentsInChildren<Transform>();

        foreach(Transform transform in meteorArray)
        {
            if(transform != this.transform)
            {
                transform.gameObject.SetActive(false);
            }
        }
    }

    private void Update()
    {
        meteorsIntensityTimer += Time.deltaTime;

        if(meteorsIntensityTimer >= meteorsIntensityTimerMax)
        {
            switch (intensityLevel)
            {
                case 1:
                    meteorArray[Random.Range(0, meteorArray.Length)].gameObject.SetActive(true);
                    meteorsIntensityTimer -= meteorsIntensityTimer;
                    break;
            }
        }
    }

    public void ActivateMeteors()
    {
        foreach (Transform transform in meteorArray)
        {
            transform.gameObject.SetActive(true);
        }
    }
}
