using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CameraTracker : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Ghost _ghost;
    [SerializeField] private Player _player;
    [SerializeField] private float _moveSpeed;

    private void OnEnable()
    {
        _ghost.GhostSpawned += OnGhostSpawned;
        _player.PlayerSpawned += OnPlayerSpawned;
    }

    private void OnDisable()
    {
        _ghost.GhostSpawned -= OnGhostSpawned;
        _player.PlayerSpawned -= OnPlayerSpawned;
    }

    private void OnGhostSpawned(Ghost ghost)
    {
        _target = ghost.transform;
    }

    private void OnPlayerSpawned(Player player)
    {
        _target = player.transform;
    }

    private void Update()
    {
        if (_target != null)
        {
            Vector3 target = new Vector3(_target.transform.position.x, _target.transform.position.y + 1, -10);

            transform.position = Vector3.Lerp(transform.position, target, _moveSpeed * Time.deltaTime);
        }
    }
}
