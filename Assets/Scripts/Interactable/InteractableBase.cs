using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableBase : MonoBehaviour
{
    public bool IsInteracting
    {
        get;
        protected set;
    }

    public bool IsHighlighted
    {
        get;
        protected set;
    }

    public abstract void InteractionStart();
    public abstract void InteractionContinue();
    public abstract void InteractionEnd();
    public abstract void Highlight();
    internal abstract void StopHighlight();
}
