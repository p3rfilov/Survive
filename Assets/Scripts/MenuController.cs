using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public Canvas mainMenu;
    public Canvas controlsMenu;
    public Canvas gameSettings;
    public Camera menuCamera;

    int loadedLevelBuildIndex;
    List<Canvas> allMenus = new List<Canvas>();

    public void BeginGame ()
    {
        menuCamera.gameObject.SetActive(false);
        HideAllMenus();
    }

    public void DisplayMainMenu ()
    {
        DisplayCanvas(mainMenu);
    }

    public void DisplayControlsMenu ()
    {
        DisplayCanvas(controlsMenu);
    }

    public void DisplaySettingsMenu ()
    {
        DisplayCanvas(gameSettings);
    }

    void Start ()
    {
        InitMenuList();
        DisplayMainMenu();
    }

    void InitMenuList ()
    {
        allMenus.Clear();
        allMenus.Add(mainMenu);
        allMenus.Add(controlsMenu);
        allMenus.Add(gameSettings);
    }

    void DisplayCanvas (Canvas canvas)
    {
        foreach (var item in allMenus)
        {
            if (item == canvas)
            {
                item.gameObject.SetActive(true);
            }
            else
            {
                item.gameObject.SetActive(false);
            }
        }
    }

    void HideAllMenus ()
    {
        foreach (var item in allMenus)
        {
            item.gameObject.SetActive(false);
        }
    }

    IEnumerator LoadLevel (int levelBuildIndex)
    {
        //this.enabled = false;
        if (loadedLevelBuildIndex > 0)
        {
            yield return SceneManager.UnloadSceneAsync(loadedLevelBuildIndex);
        }
        yield return SceneManager.LoadSceneAsync(levelBuildIndex, LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(levelBuildIndex));
        loadedLevelBuildIndex = levelBuildIndex;
        //this.enabled = true;
    }
}
