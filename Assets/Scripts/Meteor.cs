using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    private bool startMoving = false;

    private void OnEnable()
    {
        startMoving = true;
    }

    private void Update()
    {
        if (startMoving)
        {
            transform.Translate(0f, -10f * Time.deltaTime, 0f);
        }
    }
}
