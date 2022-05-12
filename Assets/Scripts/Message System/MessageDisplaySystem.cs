using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageDisplaySystem : MonoBehaviour
{
    public static MessageDisplaySystem Instance
    {
        get;
        private set;
    }

    private int idCounter;
    private List<Message> shownMessages;

    [SerializeField]
    private RectTransform parent;

    [SerializeField]
    private GameObject messagePrefab;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            idCounter = 0;
            shownMessages = new List<Message>();
            SceneTransitionManager.OnSceneLoadedEvent += DeleteAllShownMessages;
        }
    }

    private void OnDestroy()
    {
        SceneTransitionManager.OnSceneLoadedEvent -= DeleteAllShownMessages;
    }

    private void DeleteAllShownMessages(string sceneName)
    {
        foreach (Message message in shownMessages)
        {
            if (message != null)
            {
                Destroy(message.gameObject);
            }
        }

        idCounter = 0;
        shownMessages = new List<Message>();
    }

    public int ShowMessage(string message, float duration = -1)
    {
        idCounter++;
        Message newMessage = Instantiate(messagePrefab, parent).GetComponent<Message>();
        newMessage.Init(message, idCounter, duration);
        shownMessages.Add(newMessage);
        return idCounter;
    }

    public bool UpdateMessage(int id, string newMessage, float duration = -1)
    {
        Message message = shownMessages.Find(m => m.Id == id);
        if (message != null)
        {
            message.UpdateMessage(newMessage, duration);
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool RemoveMessage(int id)
    {
        Message message = shownMessages.Find(m => m.Id == id);
        if (message != null)
        {
            shownMessages.Remove(message);
            Destroy(message.gameObject);
            return true;
        }
        else
        {
            return false;
        }
    }

    [ContextMenu("Test Show")]
    private void TestShowMessage()
    {
        ShowMessage("test test test", 3f);
    }
}