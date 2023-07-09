using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    private bool startMoving = false;

    private Vector3 cachedPosition;

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Messi");

        if (collision.gameObject.TryGetComponent<PlayerStats>(out PlayerStats playerStats))
        {
            playerStats.OnDamage(10f);
        }

        gameObject.SetActive(false);
    }

    private void Awake()
    {
        cachedPosition = transform.position;
    }

    private void OnEnable()
    {
        startMoving = true;
    }

    private void OnDisable()
    {
        transform.position = cachedPosition;
        startMoving = false;
    }

    private void Update()
    {
        if (startMoving)
        {
            transform.Translate(10f * Time.deltaTime, 0f, 0f);
        }
    }

   
}
