using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckGround : MonoBehaviour
{
    public event UnityAction<bool> GroundStatusChange;

    private bool _onGround;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Ground groud))
        {
            _onGround = true;
            GroundStatusChange?.Invoke(_onGround);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Ground groud))
        {
            _onGround = false;
            GroundStatusChange?.Invoke(_onGround);
        }
    }
}
