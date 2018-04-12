using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public const int START_CHECKPOINT_ID = 0;
    public const int CHECKPOINT_COUNT = 19; //includes that starting line


    public UIController ui;
    public SimpleCarDriver currentCar;
    public GameObject simpleCarPrefab;
    public Transform startPoint;
    public GameObject camera;
    public float cameraSmoothing = 5.0f;

    private GameController2 controller;
    
    private List<GameObject> carObjects;
    private List<SimpleCarDriver> carList;
    private int NUM_CARS = 50;
    private bool running = true;
    private GeneticAlgorithm ga;


    // Use this for initialization
    void Start()
    {
        var ga = new GeneticAlgorithm(new TournamentSelection(), new CrossOverSpawnFunction());
        controller = new GameController2(ui, ga);
        
        ////
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

    // Update is called once per frame
    void Update()
    {
        if (running)
        {
            if (!AllCarsDead())
            {
                var bestCar = FindBestCar();
                if (bestCar.id != currentCar.id)
                {
                    currentCar = bestCar;
                    Debug.Log("found a better car: " + currentCar.id);
                }
            }
            else
            {
                Debug.Log("All cars are dead!");
                Debug.Log("Making a new generation!");

                var newBrains = MakeNewBrains(carList);
                foreach (var gameObject in carObjects)
                {
                    Destroy(gameObject);
                }

                carObjects = new List<GameObject>();
                carList = new List<SimpleCarDriver>();

                foreach (var brain in newBrains)
                {
                    var carObject = Instantiate(simpleCarPrefab, startPoint.position, startPoint.rotation);
                    carObjects.Add(carObject);
                    var carScript = carObject.GetComponent<SimpleCarDriver>();
                    carScript.manualControl = false;
                    carScript.controller = this;
                    carScript.brain = brain;
                    carList.Add(carScript);
                }

                currentCar = carList[0];
            }


            currentCar.SetDistances();
            ui.SetScoreText(currentCar.GetScore().ToString());
            ui.SetCarIdText(currentCar.id);

            camera.transform.position = Vector3.Lerp(camera.transform.position, currentCar.carBody.position,
                Time.deltaTime * cameraSmoothing);
        }
    }

    private List<SimpleCarBrain> MakeNewBrains(List<SimpleCarDriver> currentCars)
    {
        //convert from cars to individuals
        var currentPopulation = new List<Individual>();
        foreach (var car in currentCars)
        {
            var individual =
                new Individual {Fitness = (decimal) car.GetScore(), Id = car.id, Genome = car.brain.Genome};
            currentPopulation.Add(individual);
        }

        var newPopulation = ga.MakeNewGeneration(currentPopulation, currentPopulation.Count);

        //convert back from individuals to cars
        var newBrains = new List<SimpleCarBrain>();
        foreach (var individual in newPopulation)
        {
            var brain = new SimpleCarBrain(individual.Genome.Weights);
            newBrains.Add(brain);
        }

        return newBrains;
    }


    private bool AllCarsDead()
    {
        return !carList.Exists(c => c.IsAlive == true);
    }

    //find best car
    private SimpleCarDriver FindBestCar()
    {
        var bestCar = currentCar;
        foreach (var car in carList)
        {
            if (car.IsAlive)
            {
                if (car.GetScore() > bestCar.GetScore())
                {
                    bestCar = car;
                }
            }
        }

        return bestCar;
    }

    //find best living, or return null if all are dead
    private SimpleCarDriver FindBestLivingCar()
    {
        SimpleCarDriver bestCar = null;

        //if our current car has died, swap to the first living car
        if (currentCar.IsAlive)
        {
            bestCar = currentCar;
        }
        else
            foreach (var car in carList)
            {
                if (car.IsAlive)
                {
                    bestCar = car;
                    break;
                }
            }

        //try to find a better car
        if (bestCar != null)
        {
            foreach (var car in carList)
            {
                if (car.IsAlive)
                {
                    if (car.GetScore() > bestCar.GetScore())
                    {
                        bestCar = car;
                    }
                }
            }
        }

        return bestCar;
    }

    public void SetTurnInput(float horizontalInput)
    {
        ui.SetTurnText((horizontalInput * 100).ToString("F2"));
    }

    public void SetSpeedInput(float verticalInput)
    {
        ui.SetSpeedText((verticalInput * 100).ToString("F2"));
    }

    public void setFrontDist(float distance)
    {
        ui.SetFrontDistText((distance).ToString("F2"));
    }

    public void setFrontLeftDist(float distance)
    {
        ui.SetFrontLeftDistText((distance).ToString("F2"));
    }

    public void setFrontRightDist(float distance)
    {
        ui.SetFrontRightDistText((distance).ToString("F2"));
    }

    public void setLeftDist(float distance)
    {
        ui.SetLeftDistText((distance).ToString("F2"));
    }

    public void setRightDist(float distance)
    {
        ui.SetRightDistText((distance).ToString("F2"));
    }
}