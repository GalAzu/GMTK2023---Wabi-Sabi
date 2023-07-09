using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorShower : MonoBehaviour
{
    private Transform[] meteorArray;

    private float meteorsIntensityTimerMax = 0.7f;
    private float meteorsIntensityTimer = 0f;

    private float intensityTimer = 0f;

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
                int randomIndex = Random.Range(0, meteorArray.Length);

                if (!meteorArray[randomIndex].gameObject.activeSelf)
                {
                     meteorArray[randomIndex].gameObject.SetActive(true);
                     meteorsIntensityTimer -= meteorsIntensityTimer;
                }
        }

        intensityTimer += Time.deltaTime;

        if(intensityTimer > 20)
        {
            meteorsIntensityTimerMax -= .1f;
            intensityTimer -= intensityTimer;
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
