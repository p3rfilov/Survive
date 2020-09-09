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
    public Canvas gameOverScreen;
    public Canvas loadingScreen;
    public Camera menuCamera;

    public Slider startCount;
    public Slider spawnInterval;
    public Slider increaseInterval;
    public Slider increaseCount;
    public Slider itemDropChance;
    public Toggle bulletTime;

    bool gameRunning;
    float retryMenuDelay = 2f;
    float defaultTimeScale;
    int loadedLevelBuildIndex;
    List<Canvas> allMenus = new List<Canvas>();

    public void BeginGame ()
    {
        StartCoroutine(LoadLevel(1));
        PauseGame(false);
        DisplayCanvas(loadingScreen);
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

    public void DisplayLoadingScreen ()
    {
        DisplayCanvas(loadingScreen);
    }

    public void PauseGame (bool state)
    {
        if (state)
        {
            Time.timeScale = 0;
            DisplayMainMenu();
        }
        else
        {
            Time.timeScale = defaultTimeScale;
            HideAllMenus();
        }
        
        gameRunning = !state;
        EventManager.RaiseOnGamePaused(gameRunning);
    }

    void Awake ()
    {
        defaultTimeScale = Time.timeScale;
        EventManager.OnSomethingDied += DisplayGemeOverScreen;
    }

    void Start ()
    {
        InitMenuList();
        DisplayMainMenu();
    }

    void Update ()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            PauseGame(gameRunning);
        }
    }

    void DisplayGemeOverScreen (Transform obj)
    {
        if (obj != null && obj.tag == "Player")
        {
            StartCoroutine(DisplayGameOver());
        }
    }

    void SetGameParameters ()
    {
        EventManager.RaiseOnGameRulesChanged((int)startCount.value, spawnInterval.value, increaseInterval.value, (int)increaseCount.value, itemDropChance.value, bulletTime.isOn);
        gameRunning = true;
    }

    void InitMenuList ()
    {
        allMenus.Clear();
        allMenus.Add(mainMenu);
        allMenus.Add(controlsMenu);
        allMenus.Add(gameSettings);
        allMenus.Add(gameOverScreen);
        allMenus.Add(loadingScreen);
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

    IEnumerator DisplayGameOver ()
    {
        yield return new WaitForSeconds(retryMenuDelay);
        DisplayCanvas(gameOverScreen);
    }

    IEnumerator LoadLevel (int levelBuildIndex)
    {
        this.enabled = false;
        if (loadedLevelBuildIndex > 0)
        {
            yield return SceneManager.UnloadSceneAsync(loadedLevelBuildIndex);
        }
        yield return SceneManager.LoadSceneAsync(levelBuildIndex, LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(levelBuildIndex));
        loadedLevelBuildIndex = levelBuildIndex;
        menuCamera.gameObject.SetActive(false);
        SetGameParameters();
        HideAllMenus();
        this.enabled = true;
    }
}
