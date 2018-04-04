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
    public Text frontDistText;
    public Text frontLeftDistText;
    public Text frontRightDistText;
    public Text leftDistText;
    public Text rightDistText;
    public Text carIdText;

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

    public void SetSpeedText(string text)
    {
        speedText.text = text;
    }

    public void SetFrontDistText(string distText)
    {
        frontDistText.text = distText;
    }

    public void SetFrontLeftDistText(string distText)
    {
        frontLeftDistText.text = distText;
    }

    public void SetFrontRightDistText(string distText)
    {
        frontRightDistText.text = distText;
    }

    public void SetLeftDistText(string distText)
    {
        leftDistText.text = distText;
    }

    public void SetRightDistText(string distText)
    {
        rightDistText.text = distText;
    }

    public void SetCarIdText(string carId)
    {
        carIdText.text = carId;
    }
}