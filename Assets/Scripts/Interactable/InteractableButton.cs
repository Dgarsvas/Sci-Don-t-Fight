using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableButton : InteractableBase
{
    [SerializeField] private UnityEvent onButtonPress;
    [SerializeField] private Renderer rend;
    private Material mat;

    private void Awake()
    {
        mat = rend.material;
    }

    public override void Highlight()
    {
        IsHighlighted = true;
        rend.material = GameManager.Instance.highlightMat;
    }

    internal override void StopHighlight()
    {
        IsHighlighted = false;
        rend.material = mat;
    }

    public override void InteractionContinue()
    {
    }

    public override void InteractionEnd()
    {
    }

    public override void InteractionStart()
    {
        Debug.Log("Interaction Start");
        onButtonPress?.Invoke();
    }
}
