using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPortal : MonoBehaviour
{
    private void Start()
    {
        MessageDisplaySystem.Instance.ShowMessage("Find the portal!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PauseMenuController.Instance.ShowVictoryScreen();
        }
    }
}