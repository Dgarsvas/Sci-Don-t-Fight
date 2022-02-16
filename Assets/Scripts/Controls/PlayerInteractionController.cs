using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    [SerializeField] private Transform lookTarget;
    [SerializeField] private float interactionDistance;
    [SerializeField] LayerMask layers;

    void Update()
    {
        HandleRayCast();
    }

    private void HandleRayCast()
    {
        if (Physics.Raycast(lookTarget.position, lookTarget.rotation.eulerAngles, out RaycastHit hit, interactionDistance, layers))
        {
            IInteractable interactable = hit.transform.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.InteractionStart();
            }
        }
    }
}
