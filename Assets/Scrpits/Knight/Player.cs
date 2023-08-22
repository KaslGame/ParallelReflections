using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public int Coins => _coins;
    public int MaxHealth => _maxHealth;

    public event UnityAction<int, int> HealthChange;
    public event UnityAction<int> CoinsAmountChange;
    public event UnityAction<bool> Died;
    public event UnityAction<Player> PlayerSpawned;

    [SerializeField] private int _health;
    [SerializeField] private int _coins;

    private int _maxHealth = 100;
    private int _minHealth = 0;
    private int _countPossibleRevivals;

    private void Awake()
    {
        _health = _maxHealth;
        HealthChange?.Invoke(_health, _maxHealth);
    }

    private void OnEnable()
    {
        PlayerSpawned?.Invoke(this);
    }

    private void Start()
    {
        PlayerSpawned?.Invoke(this);
        CoinsAmountChange?.Invoke(_coins);
    }

    public void AddCountPossibleRevivals()
    {
        _countPossibleRevivals++;
    }

    public void SetMaxHealth(int maxHealth)
    {
        _maxHealth = maxHealth;
    }

    public void AddCoins(int coins)
    {
        _coins += coins;
        CoinsAmountChange?.Invoke(_coins);
    }

    public void Buy(int coins)
    {
        _coins -= coins;
        CoinsAmountChange?.Invoke(_coins);
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
        if (_countPossibleRevivals > 0)
        {
            _countPossibleRevivals--;
            Died?.Invoke(false);
        }
        else
        {
            Died?.Invoke(true);
        }
    }
}
