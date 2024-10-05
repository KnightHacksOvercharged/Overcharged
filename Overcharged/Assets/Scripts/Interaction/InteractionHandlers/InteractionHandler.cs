using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractionHandler : MonoBehaviour
{
    [Header("State")]
    public bool isActive = false;

    [Header("Energy Data")]
    [SerializeField] protected float electricityRate;

    [Header("Audio")]
    [SerializeField] protected AudioClip interactionAudioClip;

    [SerializeField]
    [Range(0, 1f)] protected float volume;
    
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
