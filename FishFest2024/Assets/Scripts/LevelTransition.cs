using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransition : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Call back from animator
    public void FinishLoading()
    {
        
    }

    public void StartLoading()
    {
        animator.SetTrigger("FadeIn");
    }
}
