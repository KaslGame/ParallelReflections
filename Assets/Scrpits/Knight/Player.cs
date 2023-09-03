using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerRespawnManager))]
public class Player : MonoBehaviour
{
    public int MaxHealth => _maxHealth;

    public event UnityAction<int, int> HealthChange;

    [SerializeField] private int _health;

    private PlayerRespawnManager _playerRespawnManager;

    private int _maxHealth = 100;
    private int _minHealth = 0;

    private void Awake()
    {
        _playerRespawnManager = GetComponent<PlayerRespawnManager>();

        _health = _maxHealth;
        HealthChange?.Invoke(_health, _maxHealth);
    }

    public void SetMaxHealth(int maxHealth)
    {
        _maxHealth = maxHealth;
    }

    public void Heal(int count)
    {
        _health = Mathf.Clamp(_health + count, _minHealth, _maxHealth);
        HealthChange?.Invoke(_health, _maxHealth);
    }

    public void TakeDamage(int damage)
    {
        _health = Mathf.Clamp(_health - damage, _minHealth, _maxHealth);

        HealthChange?.Invoke(_health, _maxHealth);

        if (_health <= 0)
            Die();
    }

    public void Die()
    {
        _playerRespawnManager.TryRespawn();
    }
}
