using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    protected PlayerManager playerManager;
    public GameObject startingZone;
    [SerializeField] Transform cameraTransform;
    [SerializeField] SpawnablesList spawnablesObject;
    [SerializeField] float spawnThresholdDistance = 3f;
    // Offset of the spawning item from the camera position to the top of the viewport
    [SerializeField] float spawnTopDistance = 9f;
    // items will spawn between horizontalSpawnRangeMin and horizontalSpawnRangeMax
    [SerializeField] float horizontalSpawnRangeMin = -3.5f;
    [SerializeField] float horizontalSpawnRangeMax = 3.5f;

    private CollidableEntity[] spawnables;
    private bool isGameActive = false;
    private Vector3 lastCameraPosition;
    private float distanceMovedUpwards = 0f;
    void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            spawnables = spawnablesObject.spawnables;
            DontDestroyOnLoad(this);
        }
    }

    void Start()
    {
        InputManager.ToggleActionMap(InputManager.inputActions.Player);
        playerManager = PlayerManager.instance;
        // Initialize lastCameraPosition with the camera's starting position
        lastCameraPosition = cameraTransform.position;
        // TODO: Add cursor lock and cursor focus
    }

    void Update()
    {
        // Check how much the camera has moved since the last frame
        float upwardsMovement = cameraTransform.position.y - lastCameraPosition.y;
        
        // Update the distance moved upwards if the camera is moving up
        if (upwardsMovement > 0)
        {
            distanceMovedUpwards += upwardsMovement;
            lastCameraPosition = cameraTransform.position;
        }

        // Check if the distance moved upwards exceeds the threshold
        if (distanceMovedUpwards >= spawnThresholdDistance)
        {
            // Reset the distance counter
            distanceMovedUpwards = 0f;
            
            // Call the method to spawn items/enemies
            SpawnEntities();
        }
    }
    public void GameStart() {
        isGameActive = true;
        playerManager.GameStart();
        startingZone.SetActive(false);
    }

    void SpawnEntities()
    {
        // Implementation of your spawning logic goes here
        Debug.Log("Spawn Entities");
        for(int i = 0; i < spawnables.Length; i++) {
            CollidableEntity spawnable = spawnables[i];
            // random number check to see if this item will be spawned
            if (UnityEngine.Random.Range(0f, 1f) > spawnable.getSpawnRate(0)) {
                continue;
            }
            Instantiate(spawnable, new Vector3(UnityEngine.Random.Range(horizontalSpawnRangeMin, horizontalSpawnRangeMax), cameraTransform.position.y + spawnTopDistance, 0), Quaternion.identity);
            break;
        }
    }
}
