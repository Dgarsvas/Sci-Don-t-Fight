using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransitionManager : MonoBehaviour
{
    public delegate void OnSceneLoadedHandler(string sceneName);
    public event OnSceneLoadedHandler OnSceneLoadedEvent;

    public bool IsTransitionActive
    {
        get; private set;
    }

    public static SceneTransitionManager Instance
    {
        get;
        private set;
    }

    [SerializeField]
    private RectTransform background;

    [SerializeField]
    private Slider loadingSlider;

    [SerializeField]
    private float animationTime;

    [SerializeField]
    private AnimationCurve curve;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            background.sizeDelta = new Vector2(Screen.width, Screen.height);
            background.anchoredPosition = new Vector2(0, Screen.height);
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
            Instance = this;
        }
    }

    private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode mode)
    {
        OnSceneLoadedEvent?.Invoke(scene.name);
        StartCoroutine(AnimateEnd());
    }

    public bool LoadScene(string sceneName)
    {
        if (IsTransitionActive)
        {
            return false;
        }

        IsTransitionActive = true;

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        StartCoroutine(LoadSceneCoroutine(operation, sceneName));
        return true;
    }

    private IEnumerator LoadSceneCoroutine(AsyncOperation operation, string sceneName)
    {
        yield return AnimateStart(operation);
    }

    private IEnumerator AnimateStart(AsyncOperation operation, Action callback = null)
    {
        float timer = 0f;
        bool doneAnimating = false;

        while (!operation.isDone)
        {
            if (timer < animationTime)
            {
                timer += Time.deltaTime;
                background.anchoredPosition = new Vector2(0, curve.Evaluate(1f - timer / animationTime) * Screen.height);
            }
            else if (!doneAnimating)
            {
                doneAnimating = true;
                background.anchoredPosition = new Vector2(0, 0);
                operation.allowSceneActivation = true;
            }
            loadingSlider.value = operation.progress;
            
            yield return null;
        }

        loadingSlider.value = 1f;
        callback?.Invoke();
    }

    private IEnumerator AnimateEnd(Action callback = null)
    {
        float timer = 0f;

        while (timer < animationTime)
        {
            timer += Time.deltaTime;
            background.anchoredPosition = new Vector2(0, curve.Evaluate(timer / animationTime) * Screen.height);
            yield return null;
        }
        background.anchoredPosition = new Vector2(0, Screen.height);

        callback?.Invoke();

        IsTransitionActive = false;
    }
    
    [ContextMenu("LoadMenu")]
    public void LoadMenu()
    {
        LoadScene("MenuScene");
    }   

    [ContextMenu("LoadTestScene")]
    public void LoadTestScene()
    {
        LoadScene("TestScene");
    }
}