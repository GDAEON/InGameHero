using System;
using System.Collections;
using System.Collections.Generic;
using Player.Scripts;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class AudioController : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private List<AudioClip> attackClips;
    [SerializeField] private List<AudioClip> dealDamageClips;
    [SerializeField] private List<AudioClip> takeDamageClips;
    [SerializeField] private List<AudioClip> deathClips;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudio(AudioClip audioClip)
    {
        _audioSource.clip = audioClip;
        _audioSource.Play();
    }
    public void AttackSound()
    {
        AudioClip audioClip = attackClips[Random.Range(0, attackClips.Count)];
        PlayAudio(audioClip);
    }
    
    public void DealDamageSound()
    {
        AudioClip audioClip = dealDamageClips[Random.Range(0, dealDamageClips.Count)];
        PlayAudio(audioClip);
    }

    public void TakeDamageSound()
    {
        AudioClip audioClip = takeDamageClips[Random.Range(0, takeDamageClips.Count)];
        PlayAudio(audioClip);
    }

    public void DeathSound()
    {
        AudioClip audioClip = deathClips[Random.Range(0, deathClips.Count)];
        PlayAudio(audioClip);
    }
}
