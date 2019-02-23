using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIUpdater : MonoBehaviour
{

    private GameObject gun;
    private GameObject ball;

    [SerializeField]
    private Text gunVelText;
    [SerializeField]
    private Text gunPosText;
    [SerializeField]
    private Text gunAccText;
    [SerializeField]
    private Text ballVelText;
    [SerializeField]
    private Text ballPosText;
    [SerializeField]
    private Text ballAccText;
    [SerializeField]
    private Button mainMenuButton;


    void Start()
    {
        gun = GameObject.FindGameObjectWithTag("Gun");
        ball = GameObject.FindGameObjectWithTag("Ball");
        mainMenuButton.onClick.AddListener(QuitToMainMenu);
    }

    void FixedUpdate()
    {
        var iVelComp = (IVelocity)gun.GetComponent(typeof(IVelocity));
        gunVelText.text = iVelComp.GetVelocity().ToString();
        gunPosText.text = gun.transform.position.ToString();
        iVelComp = (IVelocity)ball.GetComponent(typeof(IVelocity));
        ballVelText.text = iVelComp.GetVelocity().ToString();
        ballPosText.text = ball.transform.position.ToString();
        var IAccComp = (IAcceleration)gun.GetComponent(typeof(IAcceleration));
        gunAccText.text = IAccComp.GetAcceleration().ToString();
        IAccComp = (IAcceleration)ball.GetComponent(typeof(IAcceleration));
        ballAccText.text = IAccComp.GetAcceleration().ToString();
    }

    void QuitToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
