using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CheckGround))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerRespawnManager))]
public class PlayerAnimations : MonoBehaviour
{
    const string Horizontal = "Horizontal";
    const string IsGroundAnimation = "IsGround";
    const string HitAnimation = "Hit";
    const string DeathAnimation = "Death";
    const string SpeedAnimation = "Speed";
    const string JumpAnimation = "Jump";
    const string AttackAnimation = "Attack";

    [SerializeField] private Ghost _ghost;

    private Animator _animator;
    private Movement _movement;
    private Player _player;
    private CheckGround _checkGround;
    private PlayerRespawnManager _playerRespawnManager;

    private bool _onGround = false;
    private bool _isLastDeath = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _movement = GetComponent<Movement>();
        _player = GetComponent<Player>();
        _checkGround = GetComponent<CheckGround>();
        _playerRespawnManager = GetComponent<PlayerRespawnManager>();
    }

    private void OnEnable()
    {
        _checkGround.GroundStatusChange += OnGroundStatusChange;
        _player.HealthChange += OnHealthChanged;
        _playerRespawnManager.Died += OnDied;
    }

    private void OnDisable()
    {
        _checkGround.GroundStatusChange -= OnGroundStatusChange;
        _player.HealthChange -= OnHealthChanged;
        _playerRespawnManager.Died -= OnDied;
    }

    private void Update()
    {
        TryRun();
        TryJump();
        TryAttack();
    }

    public void Death()
    {
        gameObject.SetActive(false);

        if (_isLastDeath == false)
        {
            _ghost.transform.position = new Vector2(transform.position.x, transform.position.y);
            _ghost.gameObject.SetActive(true);
        }
    }

    private void OnGroundStatusChange(bool isGround)
    {
        _onGround = isGround;
        _animator.SetBool(IsGroundAnimation, _onGround);
    }

    private void OnHealthChanged(int health, int maxHealth)
    {
        _animator.SetTrigger(HitAnimation);
    }

    private void OnDied(bool isLastDeath)
    {
        _isLastDeath = isLastDeath;
        _animator.SetTrigger(DeathAnimation);
    }

    private void TryRun()
    {
        if (_movement.IsStoped == false)
        {
            float direction = Input.GetAxis(Horizontal);

            _animator.SetFloat(SpeedAnimation, Mathf.Abs(direction));

            if (!Mathf.Approximately(0, direction))
                transform.rotation = direction < 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
        }
    }

    private void TryJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _onGround && _movement.IsStoped == false)
            _animator.SetTrigger(JumpAnimation);
    }

    private void TryAttack()
    {
        if (Input.GetMouseButtonDown(0) && IsPointerOverUI() == false && _onGround && _movement.IsStoped == false)
            _animator.SetTrigger(AttackAnimation);
    }

    private bool IsPointerOverUI()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        return results.Count > 0;
    }
}