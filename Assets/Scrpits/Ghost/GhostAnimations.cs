using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class GhostAnimations : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Fly();
    }

    private void Fly()
    {
        float direction = Input.GetAxis("Horizontal");

        _animator.SetFloat("Speed", Mathf.Abs(direction));

        if (!Mathf.Approximately(0, direction))
            transform.rotation = direction > 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
    }
}
