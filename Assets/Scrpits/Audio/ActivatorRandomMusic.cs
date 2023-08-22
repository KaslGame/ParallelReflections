using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ActivatorRandomMusic : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _musics;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        ActivateRandomMusic();
    }

    private void Update()
    {
        ActivateRandomMusic();
    }

    private void ActivateRandomMusic()
    {
        if (_audioSource.isPlaying == false)
        {
            int firstIndex = 1;
            _audioSource.clip = _musics[Random.Range(firstIndex, _musics.Count - 1)];
            _audioSource.Play();
        }
    }
}
