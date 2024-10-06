using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractionHandler : MonoBehaviour
{
    [Header("State")]
    public bool isActive = false;

    [Header("Energy Data")]
    [SerializeField] protected float electricityRate;
    [SerializeField] protected int electricityAmountPerTime;

    [Header("Audio")]
    [SerializeField] protected AudioClip interactionAudioClip;

    [SerializeField]
    [Range(0, 1f)] protected float volume;
    
    [Header("Animation")]
    [SerializeField] protected bool isAnimating = false;
    protected Animator animator;

    protected virtual void Awake()
    {
        animator = this.GetComponent<Animator>();
    }

    public void OnAnimationEnd()
    {
        isAnimating = false;
    }

    protected void PlayAnimation(string parameter)
    {
        isAnimating = true;
        animator.SetTrigger(parameter);
    }

    protected void PlayInteractionSound()
    {
        if (interactionAudioClip != null)
        {
            SoundManager.Instance.PlaySoundFXClip(interactionAudioClip, this.transform, volume);
        }
    }
}
