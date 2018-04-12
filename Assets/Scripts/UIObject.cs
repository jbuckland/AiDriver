using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiObject : MonoBehaviour, IUiObject
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


    public void SetTurnText(string text)
    {
        turnText.text = text;
    }

    public void SetScoreText(string text)
    {
        scoreText.text = text;
    }


    public void SetCarIdText(string carId)
    {
        carIdText.text = carId;
    }

    public void SetSensorData(CarSensorData sensorData, string formatString)
    {
        speedText.text = sensorData.Speed.ToString(formatString);
        frontDistText.text = sensorData.FrontDistance.ToString(formatString);
        frontLeftDistText.text = sensorData.FrontLeftDistance.ToString(formatString);
        frontRightDistText.text = sensorData.FrontRightDistance.ToString(formatString);
        leftDistText.text = sensorData.LeftDistance.ToString(formatString);
        rightDistText.text = sensorData.RightDistance.ToString(formatString);
    }
}