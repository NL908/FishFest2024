using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : CollidableEntity
{
    [SerializeField]
    private float healthLost;
    [SerializeField]
    private Vector2 explosionForce;

    private float _creationTime;
    [SerializeField]
    private Vector2 movementMultiplier;

    protected override void Start()
    {
        base.Start();
        _creationTime = Time.time;
    }

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
        // TODO: Play Death particle
        if (AudioManager.instance) AudioManager.instance.PlaySound("mine_explode");
    }

    protected override void CalculateVelocity()
    {
        // Mine will move in a horizontal 8-shape
        velocity.x = Mathf.Clamp((Mathf.Sin((Time.time - _creationTime) / 2) * 2), -1, 1) * movementMultiplier.x;
        velocity.y = Mathf.Cos(Time.time - _creationTime) * movementMultiplier.y;
    }
}
