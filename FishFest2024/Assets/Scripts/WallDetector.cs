using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WallDetector : MonoBehaviour
{   
    Collider2D wallDetectCollider;
    void Awake()
    {
        wallDetectCollider = GetComponent<CircleCollider2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
        if (col.gameObject.name == "LeftWall" || col.gameObject.name == "RightWall")
        {
            AudioManager.instance.PlaySound("hit_wall");
        }
    }
}
