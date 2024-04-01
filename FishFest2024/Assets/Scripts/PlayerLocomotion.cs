using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    private Rigidbody2D _rb;
    private PlayerManager _playerManager;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerManager = GetComponent<PlayerManager>();
    }


    public void HandleMovement(Vector2 mouseMovement)
    {
        // TODO: compute the magnitude based on the movement mag and screen size?
        float mag = 1;
        Vector2 dir= -mouseMovement.normalized;
        _rb.AddForce(dir * mag, ForceMode2D.Impulse);
    }

    public void HandleRotation()
    {

    }

    public void HandleJump()
    {

    }
}
