using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Message : MonoBehaviour
{
    public int Id
    {
        get;
        private set;
    }

    [SerializeField]
    private TextMeshProUGUI text;

    private Coroutine deletionCoroutine;

    internal void Init(string message, int id, float duration)
    {
        Id = id;
        UpdateMessage(message, duration);
    }

    internal void UpdateMessage(string newMessage, float duration)
    {
        text.text = newMessage;

        if (deletionCoroutine != null)
        {
            StopCoroutine(deletionCoroutine);
        }
        if (duration > 0)
        {
            deletionCoroutine = StartCoroutine(DeleteMessageInSeconds(duration));
        }
    }

    private IEnumerator DeleteMessageInSeconds(float duration)
    {
        yield return new WaitForSeconds(duration);
        MessageDisplaySystem.Instance.RemoveMessage(Id);
    }
}