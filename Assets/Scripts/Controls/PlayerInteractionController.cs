using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    [Header("Required Fields")]
    [SerializeField] private Transform interactTarget;
    [SerializeField] private float interactionDistance;
    [SerializeField] LayerMask layers;
    [SerializeField] private KeyCode interactionKey;

    [ReadOnly] [SerializeField] private InteractableBase currentInteractable;

    private const int QUICK_ACCESS_AMOUNT = 3;

    [Header("Selection data READ-ONLY")]
    [SerializeField] private BaseUsableSO[] quickAccessUsables;
    [ReadOnly] [SerializeField] private int currentSelection;
    [ReadOnly] [SerializeField] private BaseUsableSO currentlySelected;

    [Header("Inputs")]
    [SerializeField] private KeyCode nextUsable;
    [SerializeField] private KeyCode previousUsable;
    [SerializeField] private KeyCode[] quickAccess;

    void Awake()
    {
        //quickAccessUsables = new BaseUsableSO[QUICK_ACCESS_AMOUNT];
        ChangeCurrentSelection(0);
    }


    void Update()
    {
        UpdateInteractionTarget();

        if (HandleSelectionInputs(out int newSelection))
        {
            ChangeCurrentSelection(newSelection);
        }

        HandleUseInputs();

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

    private void UpdateInteractionTarget()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2f, Screen.height/2f));
        Debug.DrawRay(ray.origin, ray.direction * 200f, Color.green);

        if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit info, 200f, layers))
        {
            interactTarget.LookAt(info.point);
        }
        else
        {
            interactTarget.LookAt(ray.GetPoint(200f));
        }
    }

    private bool HandleRayCast(out InteractableBase interactable)
    {
        Debug.DrawRay(interactTarget.position, interactTarget.forward * 200f, Color.red);
        if (Physics.Raycast(interactTarget.position, interactTarget.forward, out RaycastHit hit, interactionDistance, layers))
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

    private void HandleUseInputs()
    {
        if (currentlySelected == null)
        {
            return;
        }

        if (Input.GetMouseButtonDown((int)UsageEnum.Primary))
        {
            currentlySelected.StartUse(UsageEnum.Primary);
        }
        else if (Input.GetMouseButton((int)UsageEnum.Primary))
        {
            currentlySelected.ContinueUse(UsageEnum.Primary);
        }
        else if (Input.GetMouseButtonUp((int)UsageEnum.Primary))
        {
            currentlySelected.EndUse(UsageEnum.Primary);
        }

        if (Input.GetMouseButtonDown((int)UsageEnum.Secondary))
        {
            currentlySelected.StartUse(UsageEnum.Secondary);
        }
        else if (Input.GetMouseButton((int)UsageEnum.Secondary))
        {
            currentlySelected.ContinueUse(UsageEnum.Secondary);
        }
        else if (Input.GetMouseButtonUp((int)UsageEnum.Secondary))
        {
            currentlySelected.EndUse(UsageEnum.Secondary);
        }

        currentlySelected.BeingHeld();
    }

    private bool HandleSelectionInputs(out int newSelection)
    {
        newSelection = 0;
        return false;
        //throw new NotImplementedException();
    }

    private void ChangeCurrentSelection(int select)
    {
        currentlySelected = quickAccessUsables[select];
        currentlySelected.Setup(interactTarget);
        currentSelection = select;
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        //throw new NotImplementedException();
    }
}
