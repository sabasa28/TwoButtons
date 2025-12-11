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
        waterfallness,
        normal
    }
    PlatformModifier platformModifier;

    [SerializeField] PlatformType platformType;
    [SerializeField] int percentageOfModified;
    [SerializeField] Collider2D[] collidersToModify;
    [SerializeField] SpriteRenderer[] spritesToModify;
    [SerializeField] PhysicsMaterial2D bouncyMat;
    [SerializeField] PhysicsMaterial2D unBouncyMat;
    [SerializeField] GameObject bouncySprite;
    [SerializeField] GameObject solidSprite;
    [SerializeField] GameObject unbouncySprite;
    [SerializeField] GameObject conductiveSprite;
    [SerializeField] GameObject nonConductiveSprite;
    [SerializeField] GameObject normalSprite;
    [SerializeField] GameObject bouncyVerticalSprite;
    [SerializeField] GameObject unbouncyVerticalSprite;
    [SerializeField] GameObject conductiveVerticalSprite;
    [SerializeField] GameObject nonConductiveVerticalSprite;
    [SerializeField] GameObject normalVerticalSprite;
    [SerializeField] GameObject solidVerticalSprite;
    [SerializeField] GameObject waterfallSprite;
    [SerializeField] GameObject normalGeneralSprite;
    [SerializeField] GameObject bouncyGeneralSprite;
    [SerializeField] GameObject unbouncyGeneralSprite;

    void Start()
    {
        int randomNum = UnityEngine.Random.Range(0, 100);
        if (randomNum > percentageOfModified)
        {
            platformModifier = PlatformModifier.normal;
            Modify();
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
        GameObject instance = null;
        switch (platformModifier)
        {
            case PlatformModifier.bounciness:
                foreach (Collider2D collider in collidersToModify)
                {
                    collider.sharedMaterial = bouncyMat;
                }
                switch (platformType)
                {
                    case PlatformType.horizontal:
                        instance = Instantiate(bouncySprite);
                        break;
                    case PlatformType.vertical:
                        instance = Instantiate(bouncyVerticalSprite);
                        break;
                    case PlatformType.generalWalls:
                        instance = Instantiate(bouncyGeneralSprite);
                        break;
                    default:
                        break;
                }
                break;
            case PlatformModifier.unbounciness:
                foreach (Collider2D collider in collidersToModify)
                {
                    collider.sharedMaterial = unBouncyMat;
                }
                switch (platformType)
                {
                    case PlatformType.horizontal:
                        instance = Instantiate(unbouncySprite);
                        break;
                    case PlatformType.vertical:
                        instance = Instantiate(unbouncyVerticalSprite);
                        break;
                    case PlatformType.generalWalls:
                        instance = Instantiate(unbouncyGeneralSprite);
                        break;
                    default:
                        break;
                }
                break;
            case PlatformModifier.nonStick:
                foreach (SpriteRenderer spriteRenderer in spritesToModify)
                {
                    spriteRenderer.color = Color.yellow;
                    spriteRenderer.gameObject.tag = "NonStick";
                }
                switch (platformType)
                {
                    case PlatformType.horizontal:
                        instance = Instantiate(solidSprite);
                        break;
                    case PlatformType.vertical:
                        instance = Instantiate(solidVerticalSprite);
                        break;
                    default:
                        break;
                }
                break;
            case PlatformModifier.nonConductiveness:
                foreach (SpriteRenderer spriteRenderer in spritesToModify)
                {
                    spriteRenderer.color = Color.grey;
                    spriteRenderer.gameObject.tag = "NonConductive";
                }
                switch (platformType)
                {
                    case PlatformType.horizontal:
                        instance = Instantiate(nonConductiveSprite);
                        break;
                    case PlatformType.vertical:
                        instance = Instantiate(nonConductiveVerticalSprite);
                        break;
                    default:
                        break;
                }
                break;
            case PlatformModifier.conductiveness:
                foreach (SpriteRenderer spriteRenderer in spritesToModify)
                {
                    spriteRenderer.color = Color.cyan;
                    spriteRenderer.gameObject.tag = "Conductive";
                }
                switch (platformType)
                {
                    case PlatformType.horizontal:
                        instance = Instantiate(conductiveSprite);
                        break;
                    case PlatformType.vertical:
                        instance = Instantiate(conductiveVerticalSprite);
                        break;
                    default:
                        break;
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
                switch (platformType)
                {
                    case PlatformType.vertical:
                        instance = Instantiate(waterfallSprite);
                        break;
                    default:
                        break;
                }
                break;
            case PlatformModifier.normal:
                switch (platformType)
                {
                    case PlatformType.horizontal:
                        instance = Instantiate(normalSprite);
                        break;
                    case PlatformType.vertical:
                        instance = Instantiate(normalVerticalSprite);
                        break;
                    case PlatformType.generalWalls:
                        instance = Instantiate(normalGeneralSprite);
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
        if (instance)
        {
            instance.transform.parent = transform;
            instance.transform.localPosition = Vector2.zero;
        }
    }
}
