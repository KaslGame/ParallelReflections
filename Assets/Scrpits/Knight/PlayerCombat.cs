using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackRange;
    [SerializeField] private int _damage;

    public void UpgradeDamage(int damage)
    {
        _damage = damage;
    }

    public void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange);
        
        foreach (Collider2D hit in hitEnemies)
            if (hit.TryGetComponent(out Enemy enemy))
                enemy.TakeDamage(_damage);
    }
}
