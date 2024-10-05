using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionManager : MonoBehaviour
{
    [Header("Input System")]
    [SerializeField] private InputActionAsset inputActions;
    [SerializeField] private string interactionActionName = "Interact";

    [Header("Interaction Settings")]
    [SerializeField] private float maxDistance = 5f;

    private Camera playerCamera;

    private void Awake()
    {
        playerCamera = this.GetComponent<Camera>();
        inputActions.FindAction(interactionActionName).performed += HandleInteractionEvent;
    }

    private void HandleInteractionEvent(InputAction.CallbackContext context)
    {
        Ray ray = playerCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                interactable.OnInteract();
            }
        }
    }
}
