using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringRope : MonoBehaviour
{
    [SerializeField] Transform anchor;
    [SerializeField] Rigidbody2D anchorRb;
    [SerializeField] RopeAnchor ropeAnchor;
    [SerializeField] Transform[] bonesTransform;
    [SerializeField] RopeBone[] ropeBones;
    [SerializeField] HingeJoint2D lastJoint;
    [SerializeField] float launchStrength;
    [SerializeField] float overloadStrengthMultiplier;
    [SerializeField] float timeToCheckHit;
    bool overloadedActive = false;
    float timer = 0.0f;
    Vector2 anchorDir;
    [SerializeField] float initialSpringJointDistance;
    [SerializeField] float springJointDistanceIncreaser;
    [SerializeField] float initialSpringJointFrequency;
    [SerializeField] float springJointFrequencyIncreaser;
    [SerializeField] Color overloadedColor;
    [SerializeField] Color regularColor;
    enum RopeState
    { 
        traveling,
        stuck
    }
    RopeState ropeState = RopeState.traveling;

    void Start()
    {
        regularColor = GetComponent<SpriteRenderer>().color;
        foreach (RopeBone ropeBone in ropeBones)
        {
            ropeBone.UpdateSpringJointDistance(initialSpringJointDistance);
            ropeBone.UpdateFrequency(initialSpringJointFrequency);
        }
        foreach (Transform bone in bonesTransform)
        {
            bone.localPosition = anchor.localPosition;
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        
        switch (ropeState)
        {
            case RopeState.traveling:
                if (ropeAnchor.hitWall)
                {
                    timer = 0.0f;
                    ropeState = RopeState.stuck;
                    if (ropeAnchor.hitConductiveWall)
                    {
                        GetComponent<SpriteRenderer>().color = overloadedColor;
                    }
                    if (ropeAnchor.hitNonConductiveWall)
                    {
                        Destroy(gameObject);
                    }
                }
                foreach (RopeBone ropeBone in ropeBones)
                {
                    ropeBone.UpdateSpringJointDistance(initialSpringJointDistance + (timer * springJointDistanceIncreaser));
                }
                break;
            case RopeState.stuck:
                foreach (RopeBone ropeBone in ropeBones)
                {
                    if (initialSpringJointDistance - (timer * springJointDistanceIncreaser) > 0.005)
                    {
                        ropeBone.UpdateSpringJointDistance(initialSpringJointDistance - (timer * springJointDistanceIncreaser));
                    }
                    if (initialSpringJointFrequency + (timer * springJointFrequencyIncreaser) < 10)
                    {
                        float conductiveWallMultiplier = ropeAnchor.hitConductiveWall? 10 : 1;
                        float overloadedMultipler = overloadedActive ? 10 : 1;
                        ropeBone.UpdateFrequency(initialSpringJointFrequency + (timer * springJointFrequencyIncreaser * conductiveWallMultiplier * overloadedMultipler));
                    }
                }
                break;
        }
    }

    public void Initialize(Vector3 newAnchorDir, Rigidbody2D pulledRb, bool overloaded)
    {
        float finalLaunchStrenght = launchStrength;
        if (overloaded)
        {
            finalLaunchStrenght *= overloadStrengthMultiplier;
        }
        anchorRb.AddForce(newAnchorDir * finalLaunchStrenght);
        lastJoint.connectedBody = pulledRb;
        overloadedActive = overloaded;
        if (overloadedActive)
        {
            GetComponent<SpriteRenderer>().color = overloadedColor;
        }
        StartCoroutine(DestroyIfMiss());
    }

    IEnumerator DestroyIfMiss()
    {
        yield return new WaitForSeconds(timeToCheckHit);
        if (!ropeAnchor.hitWall)
        {
            Destroy(gameObject);
        }
    }

    public void DisableOverload()
    {
        if (overloadedActive && !ropeAnchor.hitConductiveWall)
        {
            GetComponent<SpriteRenderer>().color = regularColor;
        }
        overloadedActive = false;
    }

    public void EnableOverload()
    {
        if (!overloadedActive)
        {
            overloadedActive = true;
            GetComponent<SpriteRenderer>().color = overloadedColor;
        }
    }

}
