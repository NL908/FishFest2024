using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collidable : MonoBehaviour
{
    [SerializeField]
    protected Vector2 velocity;

    protected Rigidbody2D _rb;
    protected Collider2D _collider;
    protected PlayerManager playerManager;

    private bool _isAlive = true;

    void Start()
    {
        _rb= GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        playerManager = PlayerManager.instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isAlive && collision.gameObject.layer == 3)
        {
            // Prevent multiple bone collision
            _isAlive = false;
            HandleCollision(collision);
            HandleDeath();
            Destroy(gameObject);
            Debug.Log(collision.gameObject.name);
        }
    }
    protected abstract void HandleCollision(Collider2D collision);
    protected abstract void HandleDeath();
}
