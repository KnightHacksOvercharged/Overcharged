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

        Activate();
    }

    public void Activate()
    {
        isActive = true;

        isAnimating = true;
        animator.SetTrigger(animationTriggerParameter);

        if (interactionAudioClip != null)
        {
            SoundManager.Instance.PlaySoundFXClip(interactionAudioClip, this.transform, volume);
        }
    }

    public void Deactivate()
    {
        isActive = false;
    }
}
