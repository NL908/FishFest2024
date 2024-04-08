using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;
using UnityEngine.SceneManagement;
using EasyTransition;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    protected PlayerManager playerManager;
    public GameObject startingZone;
    private Transform cameraTransform;

    [SerializeField]
    private CinemachineVirtualCamera _virtualCamera;

    [SerializeField]
    private GameObject _aboveWaterBackgruondPrefab;

    [SerializeField]
    private TMP_Text _depthMeterText;
    [SerializeField]
    private TMP_Text _scoreText;

    [SerializeField]
    private SpriteRenderer _wallSpriteRenderer;

    [SerializeField]
    private TransitionSettings _levelTransitionSetting;

    [SerializeField] SpawnablesList spawnablesObject;
    // triggers spawn method when camera travels this amount of distance
    [SerializeField] float spawnThresholdDistance = 1f;
    // Offset of the spawning item from the camera position to the top of the viewport
    [SerializeField] float spawnTopDistance = 9f;
    // items will spawn between horizontalSpawnRangeMin and horizontalSpawnRangeMax
    [SerializeField] float horizontalSpawnRangeMin = -3.5f;
    [SerializeField] float horizontalSpawnRangeMax = 3.5f;

    // The ocean depth at the starting location. 0 is surface
    public float oceanDepth = 1000;

    public int score = 0;

    private CollidableEntity[] spawnables;
    // Array of spawn distance corresponding to the index of spawnables, it will be updated when camera moves, and when it is less than 0, the corresponding entity can be spawned
    private float[] spawnablesDistance;
    public bool isGameActive = false;
    private Vector3 lastCameraPosition;
    private float distanceMovedUpwards = 0f;

    private bool _isPlayWin = false;
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
        //InputManager.ToggleActionMap(InputManager.inputActions.Player);
        playerManager = PlayerManager.instance;
        // Initialize lastCameraPosition with the camera's starting position
        lastCameraPosition = cameraTransform.position;
        // TODO: Add cursor lock and cursor focus
        UpdateScoreTextUI();
        // Setup oceanDeath
        LockCamera lc = _virtualCamera.transform.GetComponent<LockCamera>();
        lc.maxYpos = oceanDepth;
        _wallSpriteRenderer.material.SetFloat("_OceanDepth", oceanDepth);

        Vector3 aboveWaterBkgPos = new(0, oceanDepth, 5);
        Instantiate(_aboveWaterBackgruondPrefab, aboveWaterBkgPos, Quaternion.identity);
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

    private void FixedUpdate()
    {
        UpdateDepthTextUI();
        HandleFinishGame();
    }

    public void GameStart() {
        isGameActive = true;
        playerManager.GameStart();
        startingZone.SetActive(false);

        // Activate virtual camera
        _virtualCamera.Follow = playerManager.transform;
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        isGameActive = false;
        TransitionManager.Instance().Transition(SceneManager.GetActiveScene().name, _levelTransitionSetting, 0);
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

    // This method is called when blob is land on ground from death falling animation
    // Player was unable to control before
    public void BlobLand()
    {
        // Activate Player input
        InputManager.ToggleActionMap(InputManager.inputActions.Player);

        // Enable starting line
        StartingLineScript slc = startingZone.GetComponentInChildren<StartingLineScript>();
        slc.isActive = true;
    }

    public void UpdateScoreTextUI()
    {
        _scoreText.text = string.Format("Score: {0:0}", score);
    }

    public void AddScore(int value){
        if (!isGameActive) return;
        score += value;
        UpdateScoreTextUI();
    }

    private void UpdateDepthTextUI()
    {
        float currentDepth = PlayerManager.instance.transform.position.y - oceanDepth;
        _depthMeterText.text = string.Format("{0:0} m", currentDepth);
    }

    // Check if play is above the ocean (depth)
    // If true, transite to finish game scene
    private void HandleFinishGame()
    {
        if (PlayerManager.instance.transform.position.y > oceanDepth && !_isPlayWin)
        {
            // Player beats the game!
            Debug.Log("Player Wins!");
            _isPlayWin = true;
            // TODO: Add logic here
        }
    }
}
