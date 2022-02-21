using System;
using UnityEngine;

public class PlayerToolController : MonoBehaviour
{
    private const int QUICK_ACCESS_AMOUNT = 3;

    [Header("Selection data READ-ONLY")]
    [ReadOnly] [SerializeField] private BaseUsableSO[] quickAccessUsables;
    [ReadOnly] [SerializeField] private int currentSelection;
    [ReadOnly] [SerializeField] private BaseUsableSO currentlySelected;

    [Header("Inputs")]
    [SerializeField] private KeyCode nextUsable;
    [SerializeField] private KeyCode previousUsable;
    [SerializeField] private KeyCode[] quickAccess;

    void Awake()
    {
        quickAccessUsables = new BaseUsableSO[QUICK_ACCESS_AMOUNT];
        ChangeCurrentSelection(0);
    }

    void Update()
    {
        if (HandleSelectionInputs(out int newSelection))
        {
            ChangeCurrentSelection(newSelection);
        }

        HandleUseInputs();
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
    }

    private bool HandleSelectionInputs(out int newSelection)
    {
        throw new NotImplementedException();
    }

    private void ChangeCurrentSelection(int select)
    {
        currentlySelected = quickAccessUsables[select];
        currentSelection = select;
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        throw new NotImplementedException();
    }
}
