using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] Transform player;
    float zOffset;
    [SerializeField] float lerpTValue;
    private void Start()
    {
        zOffset = transform.position.z;
    }
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(player.position.x, player.position.y, zOffset), lerpTValue);
    }
}
