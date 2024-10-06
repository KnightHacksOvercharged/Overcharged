using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitchInteractionHandler : InteractionHandler, IInteractable
{
    [Header("Attached Lights")]
    [SerializeField] private List<Light> lights = new List<Light>();

    [Header("Toggle Light Animation")]
    [SerializeField] protected string animationTriggerParameter;

    protected override void Awake()
    {
        base.Awake();
        ToggleLights();
    }

    public void OnInteract()
    {
        if (isAnimating)
        {
            return;
        }

        if (isActive)
        {
            PlayAnimation(animationTriggerParameter);
            PlayInteractionSound();
            Deactivate();
        }
        else
        {
            PlayAnimation(animationTriggerParameter);
            PlayInteractionSound();
            Activate();
        }
    }

    public void Activate()
    {
        isActive = true;
        ToggleLights();
        StartCoroutine(IUseElectricity());
    }

    public void Deactivate()
    {
        isActive = false;
        ToggleLights();
        StopAllCoroutines();
    }

    private IEnumerator IUseElectricity()
    {
        yield return new WaitForSeconds(electricityRate);

        GameManager.Instance.AddToScore(electricityAmountPerTime);

        StartCoroutine(IUseElectricity());
    }

    private void ToggleLights()
    {
        foreach (Light light in lights)
        {
            light.enabled = isActive;
        }
    }
}
