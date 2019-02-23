using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    private Text ballVelText;
    [SerializeField]
    private Text ballPosText;


    void Start()
    {
        gun = GameObject.FindGameObjectWithTag("Gun");
        ball = GameObject.FindGameObjectWithTag("Ball");
    }

    void FixedUpdate()
    {
        gunVelText.text = gun.GetComponent<Rigidbody>().velocity.ToString();
        gunPosText.text = gun.GetComponent<Rigidbody>().position.ToString();
        ballVelText.text = ball.GetComponent<Rigidbody>().velocity.ToString();
        ballPosText.text = ball.GetComponent<Rigidbody>().position.ToString();
    }
}
