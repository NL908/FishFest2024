using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField]
    private float size;
    [SerializeField]
    private float healthGained;
    [SerializeField]
    private Vector2 velocity;

    private Rigidbody2D _rb;
    private CapsuleCollider2D _collider;
    private PlayerManager playerManager = PlayerManager.instance;

    void Start()
    {
        _rb= GetComponent<Rigidbody2D>();
        _collider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check for collision with player
        if (collision.gameObject.layer == 3)
        {
            Debug.Log(collision.gameObject.name);
            // Prevent multiple bone collision
            _collider.enabled = false;
            playerManager.HandleFishCollision();
            Destroy(gameObject);
        }
    }
}
