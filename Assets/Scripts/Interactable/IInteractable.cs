using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void InteractionStart();
    void InteractionContinue();
    void InteractionEnd();
}
