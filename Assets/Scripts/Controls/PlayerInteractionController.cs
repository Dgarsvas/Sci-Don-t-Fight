using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    [SerializeField] private Transform lookTarget;
    [SerializeField] private float interactionDistance;
    [SerializeField] LayerMask layers;
    [SerializeField] private KeyCode interactionKey;

    [ReadOnly] [SerializeField] private InteractableBase currentInteractable;

    void Update()
    {
        if (HandleRayCast(out InteractableBase interactable))
        {
            currentInteractable = interactable;
            if (Input.GetKeyDown(interactionKey))
            {
                interactable.InteractionStart();
            }
            else if (Input.GetKey(interactionKey))
            {
                interactable.InteractionContinue();
            }
            else if (Input.GetKeyUp(interactionKey))
            {
                interactable.InteractionEnd();
            }
        }
        else
        {
            if (currentInteractable != null)
            {
                if (currentInteractable.IsHighlighted)
                {
                    currentInteractable.StopHighlight();
                }
                if (currentInteractable.IsInteracting)
                {
                    currentInteractable.InteractionEnd();
                }
                currentInteractable = null;
            }
        }

    }

    private bool HandleRayCast(out InteractableBase interactable)
    {
        Debug.DrawRay(lookTarget.position, lookTarget.forward * interactionDistance, Color.red);
        if (Physics.Raycast(lookTarget.position, lookTarget.forward, out RaycastHit hit, interactionDistance, layers))
        {
            interactable = hit.transform.GetComponent<InteractableBase>();
            if (interactable != null)
            {
                if (!interactable.IsHighlighted)
                {
                    interactable.Highlight();
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            interactable = null;
            return false;
        }
    }
}
