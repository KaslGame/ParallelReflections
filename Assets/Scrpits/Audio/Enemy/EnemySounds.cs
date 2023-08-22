using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EnemySounds : MonoBehaviour
{
    [SerializeField] private AudioClip _attack;
    [SerializeField] private AudioClip _dead;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void AttackSound()
    {
        _audioSource.clip = _attack;
        _audioSource.Play();
    }

    public void DieSound()
    {
        _audioSource.clip = _dead;
        _audioSource.Play();
    }
}
