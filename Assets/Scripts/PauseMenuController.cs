using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    public enum MenuState
    {
        NothingShown,
        PauseShown,
        DeathShown,
        VictoryShown
    }

    [SerializeField]
    private Camera blurCam;

    [SerializeField]
    private Material blurMat;

    [SerializeField]
    private KeyCode pauseMenuKey;

    [SerializeField]
    private GameObject menuRect;

    [SerializeField]
    private GameObject deathRect;

    [SerializeField]
    private GameObject victoryRect;

    [SerializeField]
    private GameObject nextLevelButton;

    private Transform curRespawnPoint;

    private GameObject curPlayer;

    public MenuState CurMenuState
    {
        get;
        private set;
    }

    public static PauseMenuController Instance
    {
        get;
        private set;
    }

    private CursorLockMode previousCursorLockMode;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
        CurMenuState = MenuState.NothingShown;
    }

    public void SetRespawnPoint(Transform respawnPoint)
    {
        curRespawnPoint = respawnPoint;
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
        if (CurMenuState != MenuState.DeathShown && CurMenuState != MenuState.VictoryShown)
        {
            if (Input.GetKeyDown(pauseMenuKey))
            {
                if (CurMenuState == MenuState.PauseShown)
                {
                    Hide();
                    menuRect.SetActive(false);

                }
                else
                {
                    Show();
                    CurMenuState = MenuState.PauseShown;
                    menuRect.SetActive(true);
                }
            }
        }
    }

    public void Show()
    {
        previousCursorLockMode = Cursor.lockState;
        Cursor.lockState = CursorLockMode.Confined;

        Time.timeScale = 0f;
    }

    public void Hide()
    {
        Cursor.lockState = previousCursorLockMode;
        Time.timeScale = 1f;
        CurMenuState = MenuState.NothingShown;
    }

    public void GoBackToMainMenu()
    {
        SceneTransitionManager.Instance.LoadMenu();
    }

    internal void ShowVictoryScreen()
    {
        Show();
        CurMenuState = MenuState.VictoryShown;
        victoryRect.SetActive(true);
        nextLevelButton.SetActive(SceneTransitionManager.GetLevel() < 2);
    }

    public void NextLevelClicked()
    {
        Hide();
        switch (SceneTransitionManager.GetLevel())
        {
            case 0:
                SceneTransitionManager.Instance.LoadScene(SceneTransitionManager.LEVEL_1_NAME);
                break;
            case 1:
                SceneTransitionManager.Instance.LoadScene(SceneTransitionManager.LEVEL_2_NAME);
                break;
        }
    }

    public void PlayerDied(GameObject player)
    {
        Show();
        curPlayer = player;
        CurMenuState = MenuState.DeathShown;
        deathRect.SetActive(true);
    }

    public void RespawnClicked()
    {
        Hide();
        curPlayer.transform.SetPositionAndRotation(curRespawnPoint.position, curRespawnPoint.rotation);
        curPlayer.SetActive(true);
        deathRect.SetActive(false);
    }
}