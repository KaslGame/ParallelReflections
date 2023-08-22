using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CheckGround))]
[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    const string Horizontal = "Horizontal";

    public bool IsGround => _isGround;
    public bool IsStoped => _isStoped;

    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _jumpForce = 200f;
    [SerializeField] private float _secondsStopMoving;

    private Rigidbody2D _rigidbody2D;
    private Coroutine _coroutine;
    private CheckGround _checkGround;

    private bool _isGround = false;
    private bool _isStoped = false;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _checkGround = GetComponent<CheckGround>();
    }

    private void OnEnable()
    {
        _checkGround.IsGroundChange += OnIsGroundChange;
    }

    private void OnDisable()
    {
        _checkGround.IsGroundChange -= OnIsGroundChange;
    }

    private void Update()
    {
        TryMove();
        TryJump();
    }

    private void OnIsGroundChange(bool isGround)
    {
        _isGround = isGround;
    }

    public void StopMove()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(StopMoving(_secondsStopMoving));
    }

    private void TryMove()
    {
        if (_isStoped == false)
        {
            float direction = Input.GetAxis("Horizontal");

            _rigidbody2D.velocity = new Vector2(direction * _speed, _rigidbody2D.velocity.y);
        }
        else
        {
            _rigidbody2D.velocity = new Vector2(0, 0);
        }
    }

    private void TryJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGround && _isStoped == false)
            _rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }

    private IEnumerator StopMoving(float seconds)
    {
        var time = new WaitForSeconds(seconds);

        _isStoped = true;

        yield return time;

        _isStoped = false;
    }
}