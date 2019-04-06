using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSound : MonoBehaviour
{
    AudioSource sound;
    public AudioClip collideClip;

    private void Awake()
    {
        sound = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        float randy = Random.Range(1f, 1.5f);
        sound.pitch = randy;
        sound.PlayOneShot(collideClip);

    }
}
