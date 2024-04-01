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


    public void HandleMovement()
    {
        // Movement
        
    }

    public void HandleRotation()
    {

    }

    public void HandleJump()
    {

    }
}
