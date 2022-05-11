using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UsageEnum
{
    Primary,
    Secondary
}

public abstract class BaseUsableSO : ScriptableObject
{
    [SerializeField] protected Sprite sprite;
    [TextArea] [SerializeField] protected string description;

    public abstract void StartUse(UsageEnum usageType);

    public abstract void ContinueUse(UsageEnum usageType);

    public abstract void BeingHeld();

    public abstract void EndUse(UsageEnum usageType);

    public abstract void Setup(Transform point);

    public abstract Color GetColor();
}
