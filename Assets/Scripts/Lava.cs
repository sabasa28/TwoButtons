using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float speedIncrementer;
    [SerializeField] float timeBetweenDistanceTextUpdate;
    [SerializeField] SpiderPlayer player;
    float timer = 10.0f;
    void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
        speed += speedIncrementer * Time.deltaTime;
        timer += Time.deltaTime;
        if (timer > timeBetweenDistanceTextUpdate)
        {
            timer = 0.0f;
            player.UpdateDistanceToLavaText(transform.position.y);
        }
    }
}
