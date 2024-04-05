using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    protected PlayerManager playerManager;
    public GameObject startingZone;
    private Transform cameraTransform;
    [SerializeField] SpawnablesList spawnablesObject;
    // triggers spawn method when camera travels this amount of distance
    [SerializeField] float spawnThresholdDistance = 1f;
    // Offset of the spawning item from the camera position to the top of the viewport
    [SerializeField] float spawnTopDistance = 9f;
    // items will spawn between horizontalSpawnRangeMin and horizontalSpawnRangeMax
    [SerializeField] float horizontalSpawnRangeMin = -3.5f;
    [SerializeField] float horizontalSpawnRangeMax = 3.5f;

    private CollidableEntity[] spawnables;
    // Array of spawn distance corresponding to the index of spawnables, it will be updated when camera moves, and when it is less than 0, the corresponding entity can be spawned
    private float[] spawnablesDistance;
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
            spawnablesDistance = new float[spawnables.Length];
            // Setup spawn distance array
            for (int i = 0; i < spawnables.Length; i++) {
                spawnablesDistance[i] = spawnables[i].getSpawnDistance();
            }
            DontDestroyOnLoad(this);
        }
        cameraTransform = Camera.main.transform;
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
            // update spawn distance array
            for (int i = 0; i < spawnables.Length; i++) {
                spawnablesDistance[i] -= upwardsMovement;
            }
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
        if (!isGameActive) return;
        for(int i = 0; i < spawnables.Length; i++) {
            CollidableEntity spawnable = spawnables[i];
            //check if enough distance is passed to be able to spawn this entity
            if (spawnablesDistance[i] > 0) {
                continue;
            }
            spawnablesDistance[i] = spawnable.getSpawnDistance();
            // random number check to see if this item will be spawned
            float rand = UnityEngine.Random.value;
            if (rand > spawnable.getSpawnRate(i)) {
                Debug.Log("spawning "+spawnable+" failed");
                continue;
            }

            Instantiate(spawnable, new Vector3(UnityEngine.Random.Range(horizontalSpawnRangeMin, horizontalSpawnRangeMax), cameraTransform.position.y + spawnTopDistance, 0), Quaternion.identity);
            break;
        }
    }
}
