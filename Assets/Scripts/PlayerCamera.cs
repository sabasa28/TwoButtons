using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float lerpTValue;
    [SerializeField] Vector3 offset;
    [SerializeField] float smoothTime;
    Vector3 velocity = Vector3.zero;

    private void Start()
    {
        if (CheckMobile.isMobile())
        {
            offset = new Vector3(0.0f, 0.0f, -30.0f);
        }
        else
        {
            offset = new Vector3(0.0f, 0.0f, transform.position.z);
        }
    }

    private void FixedUpdate()
    {
        Vector3 desiredPos = player.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, desiredPos, ref velocity, smoothTime, 100, Time.fixedDeltaTime);
    }
}
