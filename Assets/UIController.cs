using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text speedText;
    public Text turnText;
    public Text scoreText;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetTurnText(string text)
    {
        turnText.text = text;
    }

    public void SetScoreText(string text)
    {
        scoreText.text = text;
    }

    internal void SetSpeedText(string text)
    {
        speedText.text = text;
    }
}
