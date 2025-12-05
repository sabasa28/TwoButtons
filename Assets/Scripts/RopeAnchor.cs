using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeAnchor : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    public bool hitWall = false;
    public bool hitConductiveWall = false;
    public bool hitNonConductiveWall = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            OnHitWall();
        }
        if (collision.gameObject.CompareTag("Conductive"))
        {
            OnHitWall();
            hitConductiveWall = true;
        }
        if (collision.gameObject.CompareTag("NonConductive"))
        {
            OnHitWall();
            hitNonConductiveWall = true;
        }

    }
    void OnHitWall()
    {
        hitWall = true;
        rb.bodyType = RigidbodyType2D.Static;
    }
}
