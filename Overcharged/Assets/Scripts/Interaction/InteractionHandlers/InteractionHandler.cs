using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] protected AudioClip interactionAudio;
    
    [Header("Animation")]
    [SerializeField] protected string animationTriggerParameter;
    [SerializeField] protected bool isAnimating = false;
    protected Animator animator;

    private void Awake()
    {
        animator = this.GetComponent<Animator>();
    }

    public void OnAnimationEnd()
    {
        isAnimating = false;
    }
}
