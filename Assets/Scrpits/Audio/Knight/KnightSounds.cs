using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerRespawnManager))]
[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(AudioSource))]
public class KnightSounds : MonoBehaviour
{
    const string Horizontal = "Horizontal";

    [SerializeField] private AudioClip _run;
    [SerializeField] private AudioClip _jump;
    [SerializeField] private AudioClip _attack;
    [SerializeField] private AudioClip _died;

    private AudioSource _audioSource;
    private Movement _movement;
    private PlayerRespawnManager _playerRespawnManager;

    private float maxVolume = 1f;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _movement = GetComponent<Movement>();
        _playerRespawnManager = GetComponent<PlayerRespawnManager>();
    }

    private void OnEnable()
    {
        _playerRespawnManager.Died += OnDied;
    }

    private void OnDisable()
    {
        _playerRespawnManager.Died -= OnDied;
    }

    private void Update()
    {
        TryRunSound();
    }

    private void OnDied(bool lastDeath)
    {
        _audioSource.volume = maxVolume;
        _audioSource.loop = false;
        _audioSource.clip = _died;
        _audioSource.Play();
    }

    public void AttackSound()
    {
        _audioSource.volume = maxVolume;
        _audioSource.loop = false;
        _audioSource.clip = _attack;
        _audioSource.Play();
    }

    public void JumpSound()
    {
        _audioSource.volume = maxVolume;
        _audioSource.loop = false;
        _audioSource.clip = _jump;
        _audioSource.Play();
    }

    private void TryRunSound()
    {
        if (_movement.IsStoped == false && _movement.IsGround)
        {
            float direction = Input.GetAxis(Horizontal);

            if (Mathf.Abs(direction) > 0)
            {
                _audioSource.volume = Mathf.Abs(direction);

                if (_audioSource.isPlaying == false)
                {
                    _audioSource.loop = true;
                    _audioSource.clip = _run;
                    _audioSource.Play();
                }
            }
            else
            {
                if (_audioSource.isPlaying == true && _audioSource.clip == _run)
                {
                    StopRunSound();
                }
            }
        }
        else
        {
            if (_audioSource.isPlaying == true && _audioSource.clip == _run)
            {
                StopRunSound();
            }
        }
    }

    private void StopRunSound()
    {
        _audioSource.Stop();
        _audioSource.clip = null;
        _audioSource.loop = false;
    }
}
