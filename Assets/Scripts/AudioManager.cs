using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip loop;
    [SerializeField] AudioSource audioSource;
    void Start()
    {
        StartCoroutine(StartLoop());
    }

    IEnumerator StartLoop()
    {
        yield return new WaitUntil(() => !audioSource.isPlaying);
        audioSource.clip = loop;
        audioSource.loop = true;
        audioSource.Play();
    }

}
