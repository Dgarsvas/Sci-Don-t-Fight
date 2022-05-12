using UnityEngine;
using UnityEngine.Events;

public class InteractableSupply : InteractableBase
{
    [SerializeField] private UnityEvent onButtonPress;
    [SerializeField] private Renderer rend;

    [SerializeField] private BaseUsableSO UsableItemToGive;
    private Material mat;

    private int messageId;

    private void Awake()
    {
        mat = rend.material;
    }

    private void Start()
    {
        messageId = MessageDisplaySystem.Instance.ShowMessage("Find the freeze gun!");
    }

    public override void Highlight()
    {
        IsHighlighted = true;
        //rend.material = GameManager.Instance.highlightMat;
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
        Destroy(gameObject);
        // TODO: play sound effect
    }

    public override void InteractionStart()
    {
        Debug.Log("Interaction Start");
        MessageDisplaySystem.Instance.UpdateMessage(messageId, "Use the freeze gun!", 10);
        onButtonPress?.Invoke();
    }

    public override BaseUsableSO GiveUsable()
    {
        return UsableItemToGive;
    }
}
