using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private Vector3 direction = new Vector3(1f, 0f, 0f);

    private void Update()
    {
        transform.Translate(direction * Time.deltaTime * 10);
    }
}