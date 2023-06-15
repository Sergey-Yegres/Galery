using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        Instance = this;

        //DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += ChangeScreenOrientation;
    }
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && _sceneNumber > 0)
            GoToPreviousScene();
    }
    private int _sceneNumber { get => SceneManager.GetActiveScene().buildIndex; }
    public void GoToPreviousScene()
    {
        SceneManager.LoadScene(_sceneNumber - 1);
    }
    public void GoToNextScene()
    {
        SceneManager.LoadScene(_sceneNumber + 1);
    }
    private void ChangeScreenOrientation(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 2)
            Screen.orientation = ScreenOrientation.AutoRotation;
        else 
            Screen.orientation = ScreenOrientation.Portrait;
    }
}
