using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : CollidableEntity
{
    [SerializeField]
    private float healthGained;

    [SerializeField]
    private Vector2 fishSwimDirection;
    [SerializeField]
    private float swingMagnitude = 1f;
    [SerializeField]
    private float swingSpeed = 1f;

    private float _randomTime;

    protected override void Start()
    {
        base.Start();
        _randomTime = Random.Range(0, 3);
    }

    protected override void HandleCollision(Collider2D collision)
    {
        playerManager.HandleFishCollision(healthGained);
    }

    protected override void HandleDeath()
    {
        // TODO: Play Death particle
        if (AudioManager.instance) AudioManager.instance.PlaySound("fish_death");
    }

    protected override void CalculateVelocity()
    {
        // Fish will swim in the swim direction
        // Will also swing up & down additional to the swim direciton
        velocity.x = fishSwimDirection.x;
        velocity.y = fishSwimDirection.y + (Mathf.Cos(Time.time * swingSpeed + _randomTime)) * swingMagnitude;
    }
}
