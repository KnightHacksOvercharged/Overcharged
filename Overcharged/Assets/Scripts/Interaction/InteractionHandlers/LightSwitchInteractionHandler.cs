using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitchInteractionHandler : InteractionHandler, IInteractable
{
    public void OnInteract()
    {
        if (isAnimating)
        {
            return;
        }

        if (isActive)
        {
            PlayAnimation();
            PlayInteractionSound();
            Deactivate();
        }
        else
        {
            PlayAnimation();
            PlayInteractionSound();
            Activate();
        }
    }

    public void Activate()
    {
        isActive = true;
        StartCoroutine(IUseElectricity());
    }

    public void Deactivate()
    {
        isActive = false;
        StopAllCoroutines();
    }

    private void PlayAnimation()
    {
        isAnimating = true;
        animator.SetTrigger(animationTriggerParameter);
    }

    private void PlayInteractionSound()
    {
        if (interactionAudioClip != null)
        {
            SoundManager.Instance.PlaySoundFXClip(interactionAudioClip, this.transform, volume);
        }
    }

    private IEnumerator IUseElectricity()
    {
        yield return new WaitForSeconds(electricityRate);

        GameManager.Instance.AddToScore(electricityAmountPerTime);

        StartCoroutine(IUseElectricity());
    }
}
