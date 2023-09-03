using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Wallet : MonoBehaviour
{
    public int Coins => _coins;

    public event UnityAction<int> CoinsAmountChange;

    [SerializeField] private int _coins;

    private void Start()
    {
        CoinsAmountChange?.Invoke(_coins);
    }

    public void AddCoins(int coins)
    {
        _coins += coins;
        CoinsAmountChange?.Invoke(_coins);
    }

    public void TakeCoins(int coins)
    {
        _coins -= coins;
        CoinsAmountChange?.Invoke(_coins);
    }
}
