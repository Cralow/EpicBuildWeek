using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip breathSound;
    [SerializeField] private AudioClip attkMeleeSound;
    [SerializeField] private AudioClip attkSuicedeSound;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        IdleSound();
    }

    public void IdleSound()
    {
        if (breathSound != null) {audioSource.clip = breathSound; audioSource.Play(); }
    }

    public void AttkMeleeSound()
    {
        if (attkMeleeSound != null) audioSource.PlayOneShot(attkMeleeSound);
    }

    public void AttkSuicideSound()
    {
        if (attkSuicedeSound != null) audioSource.PlayOneShot(attkSuicedeSound);
    }
}
