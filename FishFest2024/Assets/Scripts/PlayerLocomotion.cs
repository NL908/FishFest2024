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


    public void HandleMovement(Vector2 mouseMovement)
    {
        // TODO: compute the magnitude based on the movement mag and screen size?
        Vector2 dir= -mouseMovement.normalized;
        foreach(Rigidbody2D rb in _rbs)
            rb.AddForce(dir * _jumpForce, ForceMode2D.Impulse);
    }

    public void HandleRotation()
    {

    }

    public void HandleJump()
    {

    }
}
