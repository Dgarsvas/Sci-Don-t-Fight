using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField]
    private Camera blurCam;

    [SerializeField]
    private Material blurMat;

    [SerializeField]
    private KeyCode pauseMenuKey;

    [SerializeField]
    private GameObject menuRect;

    public bool IsShown
    {
        get;
        private set;
    }

    private CursorLockMode previousCursorLockMode;

    private void Awake()
    {
        IsShown = false;
    }

    void InitializeCamera()
    {
        if (blurCam.targetTexture != null)
        {
            blurCam.targetTexture.Release();
        }

        blurCam.targetTexture = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32, 1);
        blurMat.SetTexture("_RenTex", blurCam.targetTexture);
    }

    void Update()
    {
        if (Input.GetKeyDown(pauseMenuKey))
        {
            if (IsShown)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }
    }

    public void Show()
    {
        previousCursorLockMode = Cursor.lockState;
        Cursor.lockState = CursorLockMode.Confined;

        Time.timeScale = 0f;
        IsShown = true;
        menuRect.SetActive(true);
    }

    public void Hide()
    {
        Cursor.lockState = previousCursorLockMode;
        Time.timeScale = 1f;
        IsShown = false;
        menuRect.SetActive(false);
    }

    public void SaveGameState()
    {
        Debug.LogWarning("NOT IMPLEMENTED");
    }

    public void GoBackToMainMenu()
    {
        SceneTransitionManager.Instance.LoadMenu();
    }
}
