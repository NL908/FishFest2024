using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : Collidable
{
    [SerializeField]
    private float healthLost;
    [SerializeField]
    private Vector2 explosionForce;

    protected override void HandleCollision(Collider2D collision)
    {
        playerManager.HandleMineCollision(healthLost);
        Vector2 hitDirection = collision.transform.position - transform.position;
        hitDirection.Normalize();
        hitDirection = new Vector2(hitDirection.x, Mathf.Abs(hitDirection.y));
        hitDirection = (hitDirection + Vector2.up).normalized;
        Vector2 explosionVelocity = new Vector2(hitDirection.x * explosionForce.x, hitDirection.y * explosionForce.y);
        playerManager.ChangeVelocity(explosionVelocity);
    }

    protected override void HandleDeath()
    {
        // TODO: Play Death particle and sound effect
        Debug.Log("BOOM DESU");
    }
}
