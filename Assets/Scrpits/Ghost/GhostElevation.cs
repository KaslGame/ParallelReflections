using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GhostElevation : MonoBehaviour
{
    [SerializeField] private float _heightElevation;
    [SerializeField] private float _detectionRange;
    [SerializeField] private float _speedElevation;

    private bool _isGroundDetect = false;

    private void Update()
    {
        if (Vector2.Distance(transform.position, GetGroundTransform().transform.position) <= _heightElevation && _isGroundDetect)
        {
            Vector2 elevation = new Vector2(transform.position.x, transform.position.y + _heightElevation); 

            transform.position = Vector2.MoveTowards(transform.position, elevation, _speedElevation * Time.deltaTime);
        }
    }

    private Transform GetGroundTransform()
    {
        Collider2D[] detectGrounds = Physics2D.OverlapCircleAll(transform.position, _detectionRange);

        foreach (Collider2D detectGround in detectGrounds)
            if (detectGround.TryGetComponent(out Ground ground))
            {
                _isGroundDetect = true;
                return ground.transform;
            }

        _isGroundDetect = false;
        return transform;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _detectionRange);
    }
}
