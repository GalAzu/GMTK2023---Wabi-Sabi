using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorShower : MonoBehaviour
{
    private Transform[] meteorArray;

    private void Awake()
    {
        meteorArray = GetComponentsInChildren<Transform>();

        foreach(Transform transform in meteorArray)
        {
            transform.gameObject.SetActive(false);
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
