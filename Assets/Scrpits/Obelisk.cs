using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Obelisk : MonoBehaviour
{
    const string Charge = "Charge";

    public bool IsCharge => _isCharge;

    public event UnityAction<Obelisk, bool> ObeliskEnter;

    private Animator _animator;
    private Player _player;

    private bool _isCharge;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Charged()
    {
        _animator.SetTrigger(Charge);
        _isCharge = true;
        _player.AddCountPossibleRevivals();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            ObeliskEnter?.Invoke(this, true);
            _player = player;
        }

        if (collision.TryGetComponent(out Ghost ghost))
        {
            if (_isCharge)
            {
                float riseUp = 0.5f;
                int amountHealth = 70;

                ghost.gameObject.SetActive(false);
                _player.transform.position = new Vector2(transform.position.x, transform.position.y + riseUp);
                _player.Heal(amountHealth);
                _player.gameObject.SetActive(true);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
            ObeliskEnter?.Invoke(this, false);
    }
}
