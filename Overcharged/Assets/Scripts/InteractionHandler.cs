using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionHandler : MonoBehaviour, IInteractable
{
    public void OnInteract()
    {
        Debug.Log("Interact");
    }
}
