using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    const string DeathAnimation = "Dead";
    const string HitAnimation = "Hit";
    const string WalkAnimation = "Walk";
    const string AttackAnimation = "Attack";

    [SerializeField] private int _maxHealth;
    [SerializeField] private int _damage;
    [SerializeField] private float _speed;
    [SerializeField] private float _detectionRange;
    [SerializeField] private float _attackDistance;
    [SerializeField] private float _attackRange;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackCooldown;
    [SerializeField] private float _stopDistance;
    [SerializeField] private int _reward;

    private Animator _animator;
    private Player _target;

    private int _health;
    private int _minHealth = 0;
    private float _timeBetweenAttack = 0;

    private bool _isStop = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _health = _maxHealth;
    }

    private void Update()
    {
        TryMoveToPlayer();
        TryAttack();
        Flip();
    }

    public void TakeDamage(int damage)
    {
        _health = Mathf.Clamp(_health - damage, _minHealth, _maxHealth);
        _animator.SetTrigger(HitAnimation);

        if (_health <= 0)
            _animator.SetTrigger(DeathAnimation);
    }

    public void Die()
    {
        Destroy(gameObject);
        _target.GetComponent<Wallet>().AddCoins(_reward);
    }

    private void Flip()
    {
        var direction = GetTargetTransform().transform.position.x - transform.position.x;

        if (direction > 0)
            transform.rotation = Quaternion.identity;
        else if (direction < 0)
            transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    private void TryMoveToPlayer()
    {
        if (_isStop == false)
        {
            if (Vector2.Distance(transform.position, GetTargetTransform().transform.position) > _stopDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, GetTargetTransform().transform.position, _speed * Time.deltaTime);
                _animator.SetBool(WalkAnimation, !_isStop);
            }
            else if (Vector2.Distance(transform.position, GetTargetTransform().transform.position) < _stopDistance)
            {
                transform.position = this.transform.position;
                _animator.SetBool(WalkAnimation, _isStop);
            }
        }
        else
        {
            _animator.SetBool(WalkAnimation, false);
        }
    }

    public void Attack()
    {
        Collider2D[] lookPlayers = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange);

        foreach (Collider2D lookPlayer in lookPlayers)
        {
            if (lookPlayer.TryGetComponent(out Player player))
                player.TakeDamage(_damage);
        }

        _isStop = false;
    }

    private void TryAttack()
    {
        _timeBetweenAttack += Time.deltaTime;

        if (_target != null)
        {
            if (Vector2.Distance(transform.position, _target.transform.position) <= _attackDistance && _timeBetweenAttack >= _attackCooldown)
            {
                _timeBetweenAttack = 0;
                _isStop = true;
                _animator.SetTrigger(AttackAnimation);
            }
        }
    }

    private Transform GetTargetTransform()
    {
        Collider2D[] detectPlayers = Physics2D.OverlapCircleAll(transform.position, _detectionRange);

        foreach (Collider2D lookPlayer in detectPlayers)
            if (lookPlayer.TryGetComponent(out Player player))
            {
                if (_target == null)
                    _target = player;

                return player.transform;
            }

        _target = null;
        return transform;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _detectionRange);
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }
}