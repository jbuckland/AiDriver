using System.Collections.Generic;
using UnityEngine;

public class GameController2
{
    private GeneticAlgorithm ga;

    public GameController2(IGameUI ui, IGeneticAlgorithm ga)
    {
    }

    void Init()
    {
        carList = new List<SimpleCarDriver>();
        ga = new GeneticAlgorithm(new TournamentSelection(), new CrossOverSpawnFunction());

        carObjects = new List<GameObject>();
        for (var i = 0; i < NUM_CARS; i++)
        {
            var carObject = Instantiate(simpleCarPrefab, startPoint.position, startPoint.rotation);
            carObjects.Add(carObject);
            var carScript = carObject.GetComponent<SimpleCarDriver>();
            carScript.manualControl = false;
            carScript.controller = this;
            carScript.brain = new SimpleCarBrain(); //make a brain with random weights
            carList.Add(carScript);
        }

        //pick the first car to start watching
        if (carList.Count > 0)
        {
            currentCar = carList[0];
            //currentCar.manualControl = true;
        }
    }
}

public interface IGeneticAlgorithm
{
    List<Individual> MakeNewGeneration(List<Individual> population, int numInNewPopulation);
    void Mutate(Individual individual, double mutationSeverity);
}

public interface IGameUI
{
    void SetTurnText(string text);
    void SetScoreText(string text);
    void SetSpeedText(string text);
    void SetFrontDistText(string distText);
    void SetFrontLeftDistText(string distText);
    void SetFrontRightDistText(string distText);
    void SetLeftDistText(string distText);
    void SetRightDistText(string distText);
    void SetCarIdText(string carId);
}