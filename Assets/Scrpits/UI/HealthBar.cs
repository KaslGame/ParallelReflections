using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthBar : Bar
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _healthText;

    private void OnEnable()
    {
        _player.HealthChange += OnValueChanged;
        _player.HealthChange += OnHealthChange;
    }

    private void OnDisable()
    {
        _player.HealthChange -= OnValueChanged;
        _player.HealthChange -= OnHealthChange;
    }

    private void OnHealthChange(int health, int maxHealth)
    {
        _healthText.text = health + "/" + maxHealth;
    }
}
