using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitchInteractionHandler : InteractionHandler, IInteractable
{
    public void OnInteract()
    {
        if (!isAnimating)
        {
            isAnimating = true;
            animator.SetTrigger(animationTriggerParameter);
        }
    }
}
