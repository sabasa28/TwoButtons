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
    [SerializeField] float overloadTime;
    [SerializeField] float jetpackTime;
    [SerializeField] Sprite overloadSprite;
    [SerializeField] Sprite jetpackSprite;

    void Start()
    {
        int rand = Random.Range(0, 4);
        if (rand > 1)
        {
            Destroy(gameObject);
        }
        else
        { 
            powerUpType = (PowerUpType)rand;
            switch (powerUpType)
            {
                case PowerUpType.Overload:
                    GetComponent<SpriteRenderer>().sprite = overloadSprite;
                    break;
                case PowerUpType.Jetpack:
                    GetComponent<SpriteRenderer>().sprite = jetpackSprite;
                    break;
                default:
                    break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            switch (powerUpType)
            {
                case PowerUpType.Overload:
                    collision.gameObject.GetComponent<SpiderPlayer>().Overload(overloadTime);
                    break;
                case PowerUpType.Jetpack:
                    collision.gameObject.GetComponent<SpiderPlayer>().EquipJetpack(jetpackTime);
                    break;
            }
            Destroy(gameObject);

        }
    }
}
