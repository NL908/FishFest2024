using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : CollidableEntity
{
    [SerializeField]
    private float healthGained = 1;
    [SerializeField]
    private float currencyGained = 1;

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
        // flip the sprite based on x direction
        if (fishSwimDirection.x > 0) GetComponent<SpriteRenderer>().flipX = true;
        ChangeSpriteColorToRandom();
    }

    protected override void HandleCollision(Collider2D collision)
    {
        playerManager.HandleFishCollision(healthGained, currencyGained);
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

    void ChangeSpriteColorToRandom()
    {
        // Get the SpriteRenderer component from the entity
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            // Generate a random color
            Color randomColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            
            // Set the sprite renderer's color to the newly generated random color
            spriteRenderer.color = randomColor;
        }
    }
}
