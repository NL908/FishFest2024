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
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            float hue = Random.Range(0f, 1f);

            float saturation = Random.Range(0.95f, 1f);
            float brightness = Random.Range(0.95f, 1f);

            Color tropicalColor = Color.HSVToRGB(hue, saturation, brightness);

            spriteRenderer.color = tropicalColor;
        }
    }
}
