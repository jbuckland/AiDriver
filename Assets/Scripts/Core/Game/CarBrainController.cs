using System;
using System.Collections.Generic;
using UnityEngine;

public interface ICarObject
{
    CarSensorData GetSensorData();
    float GetHorizontalInput();
    float GetVerticalInput();
    void Move(float horizInput, float vertInput, float deltaTime);
}

public interface ICarBrainController
{
    decimal SpeedOutput { get; }
    decimal TurnOutput { get; }
    Genome Genome { get; set; }
    int CarObjectId { get; set; }
    bool IsAlive { get; set; }
    int Score { get; set; }
    string Id { get; set; }
    ICarObject CarObject { get; set; }
    bool ManualControl { get; set; }
    CarSensorData GetSensorData();
    void FixedUpdate(float deltaTime);
    void HitWall();
    void HitCheckpoint(int id);
}

public class CarBrainController : ICarBrainController
{
    public decimal SpeedOutput { get; private set; }
    public decimal TurnOutput { get; private set; }
    public Genome Genome { get; set; }
    public int CarObjectId { get; set; }
    public bool IsAlive { get; set; }
    public int Score { get; set; }
    public string Id { get; set; }
    public ICarObject CarObject { get; set; }
    public bool ManualControl { get; set; }

    private List<int> touchedCheckPoints = new List<int>();
    private const int POINTS_PER_CHECKPOINT = 1;
    private NeuralNetwork neuralNetwork;
    private float KILL_TIME = 10;
    private float timeSinceLastScore = 0;

    public CarBrainController() : this(null)
    {
    }


    public CarBrainController(NeuralNetwork neuralNetwork)
    {
        Id = Guid.NewGuid().ToString().Substring(0, 5);
        IsAlive = true;
        this.neuralNetwork = neuralNetwork;
        
    }


    public CarSensorData GetSensorData()
    {
        return CarObject.GetSensorData();
    }

    public void FixedUpdate(float deltaTime)
    {
        if (IsAlive)
        {
            var sensorData = GetSensorData();
            float horizontalInput = 0;
            float verticalInput = 0;
            if (ManualControl == true)
            {
                horizontalInput = CarObject.GetHorizontalInput();
                verticalInput = CarObject.GetVerticalInput();
            }
            else
            {
                Evaluate(sensorData);
                horizontalInput = (float) SpeedOutput;
                verticalInput = (float) TurnOutput;
            }


            Move(horizontalInput, verticalInput, deltaTime);
            CheckForTimeoutKill(deltaTime);
        }
    }

    private void Move(float horizInput, float vertInput, float deltaTime)
    {
        CarObject.Move(horizInput, vertInput, deltaTime);
    }

    private void Evaluate(CarSensorData sensorData)
    {
        var outputs = neuralNetwork.CalculateOutputs(new List<decimal>
        {
            sensorData.FrontDistance,
            sensorData.FrontLeftDistance,
            sensorData.FrontRightDistance,
            sensorData.LeftDistance,
            sensorData.RightDistance,
            sensorData.Speed
        });
        var speedUp = outputs[0];
        var speedDown = outputs[1];
        var turnLeft = outputs[2];
        var turnRight = outputs[3];

        SpeedOutput = 0;
        if (speedUp + speedDown != 0)
        {
            SpeedOutput = (speedUp - speedDown) / (speedUp + speedDown);
        }

        TurnOutput = 0;
        if (turnLeft + turnRight != 0)
        {
            TurnOutput = (turnRight - turnLeft) / (turnRight + turnLeft);
        }
    }

    public void HitWall()
    {
        IsAlive = false;
    }

    public void HitCheckpoint(int id)
    {
        //Debug.Log("We hit checkpoint " + id);

        //if we have touched all other checkpoints, and we're now touching the start
        //reset checkpoints touched, and add start line.
        if (id == MainGameObject.START_CHECKPOINT_ID && touchedCheckPoints.Count == MainGameObject.CHECKPOINT_COUNT)
        {
            touchedCheckPoints.Clear();
            ScoreCheckPoint(id);
        }
        //if we haven't touched this checkpoint yet, record that we've touched it
        else if (!touchedCheckPoints.Contains(id))
        {
            ScoreCheckPoint(id);
        }
    }

    private void ScoreCheckPoint(int id)
    {
        touchedCheckPoints.Add(id);
        Score += POINTS_PER_CHECKPOINT;
        timeSinceLastScore = 0;
    }

    private void CheckForTimeoutKill(float deltaTime)
    {
        //kill the car if it's score doesn't get better for a fixed number of seconds
        timeSinceLastScore += deltaTime;
        if (ManualControl == false && timeSinceLastScore > KILL_TIME)
        {
            IsAlive = false;
        }
    }
}