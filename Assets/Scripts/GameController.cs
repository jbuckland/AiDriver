using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public const int START_CHECKPOINT_ID = 0;
    public const int CHECKPOINT_COUNT = 18; //includes that starting line


    public UIController ui;
    public SimpleCarDriver currentCar;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ui.SetScoreText(currentCar.GetScore().ToString());
    }

    public void SetTurnInput(float horizontalInput)
    {
        ui.SetTurnText((horizontalInput * 100).ToString("0"));
    }

    public void SetSpeedInput(float verticalInput)
    {
        ui.SetSpeedText((verticalInput * 100).ToString("0"));

    }

    public void setFrontDist(float distance)
    {
        ui.SetFrontDistText((distance).ToString("0"));
    }

    public void setFrontLeftDist(float distance)
    {
        ui.SetFrontLeftDistText((distance).ToString("0"));
    }

    public void setFrontRightDist(float distance)
    {
        ui.SetFrontRightDistText((distance).ToString("0"));
    }

    public void setLeftDist(float distance)
    {
        ui.SetLeftDistText((distance).ToString("0"));
    }

    public void setRightDist(float distance)
    {
        ui.SetRightDistText((distance).ToString("0"));
    }
}
