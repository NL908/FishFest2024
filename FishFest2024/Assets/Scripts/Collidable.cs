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

    void Start()
    {
        _rb= GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        playerManager = PlayerManager.instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            // Prevent multiple bone collision
            _collider.enabled = false;
            HandleCollision();
            PlayDeathAnimation();
            Destroy(gameObject);
            Debug.Log(collision.gameObject.name);
        }
    }
    protected abstract void HandleCollision();
    protected abstract void PlayDeathAnimation();
}
