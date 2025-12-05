using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeBone : MonoBehaviour
{
    [SerializeField] SpringJoint2D springJoint;

    public void UpdateSpringJointDistance(float distance)
    {
        springJoint.distance = distance;
    }

    public void UpdateFrequency(float frequency)
    {
        springJoint.frequency = frequency;
    }
}
