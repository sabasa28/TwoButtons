using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpContainer : MonoBehaviour
{
    enum PowerUpType
    {
        Overload,
        Jetpack
    }
    PowerUpType powerUpType;

    void Start()
    {
        powerUpType = (PowerUpType)Random.Range(0, 2);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (powerUpType)
        {
            case PowerUpType.Overload:
                break;
            case PowerUpType.Jetpack:
                break;
        }
        Destroy(gameObject);
    }
}
