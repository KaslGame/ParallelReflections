using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Player))]
public class PlayerRespawnManager : MonoBehaviour
{
    public event UnityAction<bool> Died;
    public event UnityAction<Player> PlayerSpawned;

    private int _countPossibleRevivals;

    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        PlayerSpawned?.Invoke(_player);
    }

    private void Start()
    {
        PlayerSpawned?.Invoke(_player);
    }

    public void AddCountPossibleRevivals()
    {
        _countPossibleRevivals++;
    }

    public void TryRespawn()
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
