using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public class RandomPlatform : MonoBehaviour
{
    [Serializable]
    public enum PlatformType
    {
        horizontal,
        vertical,
        generalWalls
    }

    enum PlatformModifier
    {
        bounciness,
        unbounciness,
        nonStick,
        nonConductiveness,
        conductiveness,
        waterfallness
    }
    PlatformModifier platformModifier;

    [SerializeField] PlatformType platformType;
    [SerializeField] int percentageOfModified;
    [SerializeField] Collider2D[] collidersToModify;
    [SerializeField] SpriteRenderer[] spritesToModify;
    [SerializeField] PhysicsMaterial2D bouncyMat;
    [SerializeField] PhysicsMaterial2D unBouncyMat;


    void Start()
    {
        int randomNum = UnityEngine.Random.Range(0, 100);
        if (randomNum > percentageOfModified)
        {
            return;
        }
        switch (platformType)
        {
            case PlatformType.horizontal:
                randomNum = UnityEngine.Random.Range(0, 5);
                break;
            case PlatformType.vertical:
                randomNum = UnityEngine.Random.Range(0, 6);
                break;
            case PlatformType.generalWalls:
                randomNum = UnityEngine.Random.Range(0, 2);
                break;
            default:
                break;
        }
        platformModifier = (PlatformModifier)randomNum;
        Modify();
    }

    void Modify()
    {
        switch (platformModifier)
        {
            case PlatformModifier.bounciness:
                foreach (Collider2D collider in collidersToModify)
                {
                    collider.sharedMaterial = bouncyMat;
                }
                foreach (SpriteRenderer spriteRenderer in spritesToModify)
                {
                    spriteRenderer.color = Color.magenta;
                }
                break;
            case PlatformModifier.unbounciness:
                foreach (Collider2D collider in collidersToModify)
                {
                    collider.sharedMaterial = unBouncyMat;
                }
                foreach (SpriteRenderer spriteRenderer in spritesToModify)
                {
                    spriteRenderer.color = Color.green;
                }
                break;
            case PlatformModifier.nonStick:
                foreach (SpriteRenderer spriteRenderer in spritesToModify)
                {
                    spriteRenderer.color = Color.yellow;
                    spriteRenderer.gameObject.tag = "NonStick";
                }
                break;
            case PlatformModifier.nonConductiveness:
                foreach (SpriteRenderer spriteRenderer in spritesToModify)
                {
                    spriteRenderer.color = Color.grey;
                    spriteRenderer.gameObject.tag = "NonConductive";
                }
                break;
            case PlatformModifier.conductiveness:
                foreach (SpriteRenderer spriteRenderer in spritesToModify)
                {
                    spriteRenderer.color = Color.cyan;
                    spriteRenderer.gameObject.tag = "Conductive";
                }
                break;
            case PlatformModifier.waterfallness:
                foreach (SpriteRenderer spriteRenderer in spritesToModify)
                {
                    spriteRenderer.color = Color.blue;
                    spriteRenderer.gameObject.tag = "StunningWaterfall";
                }
                foreach (Collider2D collider in collidersToModify)
                {
                    collider.isTrigger = true;
                }
                break;
            default:
                break;
        }
    }
}
