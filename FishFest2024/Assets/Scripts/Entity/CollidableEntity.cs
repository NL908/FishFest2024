using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollidableEntity : MonoBehaviour
{
    [SerializeField]
    protected Vector2 velocity;
    [SerializeField] protected float baseSpawnRate = 1f;
    // a spawn check of this entity can be performed after camera moves this much distance
    [SerializeField] protected float spawnDistance = 3f;

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
            _isAlive = false;
            HandleCollision(collision);
            HandleDeath();
            Destroy(gameObject);
            Debug.Log(gameObject.name + " detects collision with " + collision.gameObject.name);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    // Can be overridden in child classes to return a modified spawn rate
    public float getSpawnRate(int currentScore = 0) {
        return baseSpawnRate;
    }

    public float getSpawnDistance() {
        return spawnDistance;
    }
    protected abstract void HandleCollision(Collider2D collision);
    protected abstract void HandleDeath();
}
