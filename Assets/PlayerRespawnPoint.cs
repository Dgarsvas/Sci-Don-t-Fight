using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawnPoint : MonoBehaviour
{
    void Start()
    {
        PauseMenuController.Instance.SetRespawnPoint(transform);
    }
}
