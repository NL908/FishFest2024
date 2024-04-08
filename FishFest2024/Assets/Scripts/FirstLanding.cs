using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstLanding : MonoBehaviour
{
    private ParticleSystem _landingParticle;
    private bool _isActive = true;

    [SerializeField]
    private Transform groundTransform;

    private void Awake()
    {
        _landingParticle = GetComponent<ParticleSystem>();

        //if (groundTransform == null)
        //{
        //    Debug.LogWarning("The ground transform has not been set in " + gameObject.name + "!");
        //}
        //else
        //{
        //    _landingParticle.collision.AddPlane(groundTransform);
        //}
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Our Blob LANDS!
        if (_isActive && collision.tag == "Player")
        {
            AudioManager.instance.PlaySound("player_landing");
            _isActive = false;
            _landingParticle.Play();
            var shape = _landingParticle.shape;
            Vector3 shapePos = shape.position;
            float x = transform.InverseTransformPoint(collision.transform.position).x;
            shape.position = new Vector3(x, shapePos.y, shapePos.z);
            _landingParticle.Play();
            PlayerManager.instance.isGameOverIfFallOffScreen = true;
            GameManager.instance.BlobLand();
        }
    }
}
