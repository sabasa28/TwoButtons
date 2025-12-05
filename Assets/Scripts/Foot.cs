using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foot : MonoBehaviour
{
    [SerializeField] List<GameObject> collidingObjects = new List<GameObject>();
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collidingObjects.Contains(collision.gameObject))
        {
            collidingObjects.Add(collision.gameObject);    
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collidingObjects.Contains(collision.gameObject))
        {
            collidingObjects.Remove(collision.gameObject);
        }
    }

    public bool IsColliding()
    {
        return collidingObjects.Count > 0;
    }

}
