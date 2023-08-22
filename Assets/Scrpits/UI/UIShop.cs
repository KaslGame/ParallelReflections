using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIShop : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _priceTextHealth;
    [SerializeField] private TMP_Text _priceTextDamage;

    private int _maxHealthUpgrade = 2;
    private int _maxDamageUpgrade = 2;
    private int _currentHealthUpgrade = 0;
    private int _currentDamageUpgrade = 0;

    private List<int> _costsHealthUpgrade = new List<int>()
    {
        20, 50, 120
    };

    private List<int> _costsDamageUpgrade = new List<int>()
    {
        30, 60, 220
    };

    private List<int> _amountHealthUpgrade = new List<int>()
    {
        115, 130, 150
    };

    private List<int> _amountDamageUpgrade = new List<int>()
    {
        5, 7, 10
    };

    private void Awake()
    {
        _priceTextHealth.text = _costsHealthUpgrade[_currentHealthUpgrade].ToString();
        _priceTextDamage.text = _costsDamageUpgrade[_currentDamageUpgrade].ToString();
    }

    public void UpgradeHealth()
    {
        if (_currentHealthUpgrade <= _maxHealthUpgrade)
        {
            if (_player.Coins >= _costsHealthUpgrade[_currentHealthUpgrade])
            {
                _player.SetMaxHealth(_amountHealthUpgrade[_currentHealthUpgrade]);
                _player.Heal(_player.MaxHealth);
                _player.Buy(_costsHealthUpgrade[_currentHealthUpgrade]);
                _currentHealthUpgrade++;
                _priceTextHealth.text = _costsHealthUpgrade[_currentHealthUpgrade].ToString();
            }
            else
            {
                Debug.Log("Не хватает денег");
            }
        }
    }

    public void UpgradeDamage()
    {
        if (_currentHealthUpgrade <= _maxDamageUpgrade)
        {
            if (_player.Coins >= _costsDamageUpgrade[_currentDamageUpgrade])
            {
                _player.GetComponent<PlayerCombat>().UpgradeDamage(_amountDamageUpgrade[_currentDamageUpgrade]);
                _player.Buy(_costsDamageUpgrade[_currentDamageUpgrade]);
                _currentDamageUpgrade++;
                _priceTextDamage.text = _costsDamageUpgrade[_currentDamageUpgrade].ToString();
            }
            else
            {
                Debug.Log("Не хватает денег");
            }
        }
    }
}
