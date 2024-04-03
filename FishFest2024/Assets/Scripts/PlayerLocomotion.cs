using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    private Rigidbody2D[] _rbs;
    private PlayerManager _playerManager;

    [SerializeField]
    private float _jumpForce = 5f;

    private void Awake()
    {
        _rbs = GetComponentsInChildren<Rigidbody2D>();
        _playerManager = GetComponent<PlayerManager>();
    }

    public void HandleJump(Vector2 moveDirection)
    {
        ChangeVelocity(moveDirection * _jumpForce);
    }

    public void ChangeVelocity(Vector2 newVelocity)
    {
        foreach (Rigidbody2D rb in _rbs)
            rb.velocity = newVelocity;
    }
}
