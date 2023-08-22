using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckGround : MonoBehaviour
{
    public event UnityAction<bool> IsGroundChange;

    private bool _isGround;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Ground groud))
        {
            _isGround = true;
            IsGroundChange?.Invoke(_isGround);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Ground groud))
        {
            _isGround = false;
            IsGroundChange?.Invoke(_isGround);
        }
    }
}
