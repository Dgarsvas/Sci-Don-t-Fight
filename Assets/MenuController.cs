using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    private string lastLevel;

    public void Awake()
    {
        lastLevel = PlayerPrefs.GetString("lastLevel", "InteractionTutorialLevel"); //TODO make an actual save system
    }

    public void StartLastLevel()
    {
        StartLevel(lastLevel);
    }

    public void StartLevel(string sceneName)
    {
        SceneTransitionManager.Instance.LoadScene(sceneName);
    }
}
