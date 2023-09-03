using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CoinsBar : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private TMP_Text _coinsText;
    [SerializeField] private AudioClip _coinSound;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _wallet.CoinsAmountChange += OnCoinsChange;
    }

    private void OnDisable()
    {
        _wallet.CoinsAmountChange -= OnCoinsChange;
    }

    private void OnCoinsChange(int coins)
    {
        _coinsText.text = coins.ToString();
        ActivateCoinSound();
    }

    private void ActivateCoinSound()
    {
        _audioSource.clip = _coinSound;
        _audioSource.Play();
    }
}
