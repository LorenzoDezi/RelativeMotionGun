using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuComponent : MonoBehaviour
{
    [SerializeField]
    private Button quitButton;
    [SerializeField]
    private Button dynamicsForceSceneButton;
    [SerializeField]
    private Button dynamicsSceneButton;
    [SerializeField]
    private Button kinematicsSceneButton;

    private void Start()
    {
        quitButton.onClick.AddListener(Quit);
        dynamicsForceSceneButton.onClick.AddListener(delegate { SetActiveScene("DynamicsForcesScene"); });
        dynamicsSceneButton.onClick.AddListener(delegate { SetActiveScene("DynamicsScene"); });
        kinematicsSceneButton.onClick.AddListener(delegate { SetActiveScene("KinematicsScene"); });


    }

    public static void Quit()
    {
        Application.Quit();
    }

    public static void SetActiveScene(string sceneName)
    {
        //TODO: Loading screen
        SceneManager.LoadScene(sceneName);
    }
}
