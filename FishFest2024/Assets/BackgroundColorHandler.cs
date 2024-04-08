using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundColorHandler : MonoBehaviour
{
    private GameManager _gameManager;

    [SerializeField]
    private SpriteRenderer backgroundsr;

    private float oceanDepth;

    // The 
    [SerializeField]
    private float endColorBufferDistance = 10f;

    // The starting color at the bottom of the ocean
    private Color _startColor;
    // The background color when blob is at surface
    [SerializeField]
    private Color _endColor;

    private void Awake()
    {
        _gameManager = GetComponent<GameManager>();

        oceanDepth = _gameManager.oceanDepth;
    }

    private void Start()
    {
        _startColor = backgroundsr.color;
    }

    public void AdjustBackgroundColor()
    {
        backgroundsr.color = Color.Lerp(
            _startColor, _endColor,
            Mathf.Clamp01(Camera.main.transform.position.y / (oceanDepth - endColorBufferDistance))
            );
        Debug.Log(Mathf.Clamp01(Camera.main.transform.position.y / (oceanDepth - endColorBufferDistance)));
    }

    private void Update()
    {
        AdjustBackgroundColor();
    }

}
