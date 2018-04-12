using System.Collections.Generic;
using UnityEngine;

public class GameController
{
    private int NUM_CARS = 50;

    private IUiObject uiObject;
    private IMainGameObject mainGameObject;

    private IGeneticAlgorithm ga;
    
    private List<ICarBrainController> brainControllerList;
    private ICarBrainController currentBrainController;

    private bool running = true;

    public GameController(IUiObject uiObject, IGeneticAlgorithm ga, IMainGameObject mainGameObject)
    {
        this.uiObject = uiObject;
        this.ga = ga;
        this.mainGameObject = mainGameObject;
    }

    public void Init()
    {
        brainControllerList = new List<ICarBrainController>();
        for (var i = 0; i < NUM_CARS; i++)
        {
            brainControllerList.Add(mainGameObject.CreateCarBrainController(null)); //make a brain with random weights
        }

        SetupGenerationOfBrains(brainControllerList);
    }

    public void Update(float deltaTime)
    {
        if (running)
        {
            if (!AllCarsDead())
            {
                var bestBrain = FindBestBrain();
                if (bestBrain.Id != currentBrainController.Id)
                {
                    currentBrainController = bestBrain;
                    Debug.Log("found a better car: " + currentBrainController.Id);
                }
            }
            else
            {
                Debug.Log("All cars are dead!");
                Debug.Log("Making a new generation!");
                var newBrains = MakeNewGenerationOfBrains(brainControllerList);
                SetupGenerationOfBrains(newBrains);
            }

            var sensorData = currentBrainController.GetSensorData();
            uiObject.SetSensorData(sensorData, "F2");
            uiObject.SetScoreText(currentBrainController.Score.ToString());
            uiObject.SetCarIdText(currentBrainController.Id);

            mainGameObject.CameraFollowCar(currentBrainController.CarObjectId, deltaTime);
        }
    }


    private void SetupGenerationOfBrains(List<ICarBrainController> newBrains)
    {
        brainControllerList = newBrains;
        foreach (var brain in brainControllerList)
        {
            mainGameObject.DestroyCarObject(brain.CarObjectId);
        }

        mainGameObject.ClearCarObjects();

        foreach (var brain in newBrains)
        {
            var carId = mainGameObject.InstantiateCar(brain, false);
            brain.CarObjectId = carId;
            brainControllerList.Add(brain);
        }

        //pick the first car to start watching
        if (brainControllerList.Count > 0)
        {
            currentBrainController = brainControllerList[0];
        }
    }

    private List<ICarBrainController> MakeNewGenerationOfBrains(List<ICarBrainController> currentBrains)
    {
        //convert from cars to individuals
        var currentPopulation = new List<Individual>();
        foreach (var brain in currentBrains)
        {
            var individual =
                new Individual {Fitness = (decimal) brain.Score, Id = brain.Id, Genome = brain.Genome};
            currentPopulation.Add(individual);
        }

        var newPopulation = ga.MakeNewGeneration(currentPopulation, currentPopulation.Count);

        //convert back from individuals to cars
        var newBrains = new List<ICarBrainController>();
        foreach (var individual in newPopulation)
        {
            var brain = mainGameObject.CreateCarBrainController(individual.Genome.Weights);
            newBrains.Add(brain);
        }

        return newBrains;
    }

    private bool AllCarsDead()
    {
        return !brainControllerList.Exists(c => c.IsAlive == true);
    }

    //find best car
    private ICarBrainController FindBestBrain()
    {
        var bestBrain = currentBrainController;
        foreach (var brain in brainControllerList)
        {
            if (brain.IsAlive)
            {
                if (brain.Score > bestBrain.Score)
                {
                    bestBrain = brain;
                }
            }
        }

        return bestBrain;
    }
}

public interface IGeneticAlgorithm
{
    List<Individual> MakeNewGeneration(List<Individual> population, int numInNewPopulation);
    void Mutate(Individual individual, double mutationSeverity);
}

public interface IMainGameObject
{
    int InstantiateCar(ICarBrainController brainController, bool manualControl);
    void DestroyCarObject(int carObjectId);
    void ClearCarObjects();
    void CameraFollowCar(int carObjectId, float deltaTime);
    ICarBrainController CreateCarBrainController(List<decimal> genomeWeights);
}

public interface IUiObject
{
    void SetTurnText(string text);
    void SetScoreText(string text);
    void SetCarIdText(string carId);
    void SetSensorData(CarSensorData sensorData, string formatString);
}