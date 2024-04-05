using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovingHorizontal : CollidableEntity
{
    [SerializeField]
    private float healthGained;
    [SerializeField] private float speed = 1f;
    private bool isGoingLeft = true;

    void Awake() {
        // randomize going left or right
        if (Random.value < 0.5) {
            isGoingLeft = false;
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    void Update() {
        float velocity = speed;
        if (isGoingLeft) velocity *= -1;
        transform.position += new Vector3(velocity * Time.deltaTime, 0, 0);
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
}
