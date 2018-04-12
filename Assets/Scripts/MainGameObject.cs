using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class MainGameObject : MonoBehaviour, IMainGameObject
{
    public const int START_CHECKPOINT_ID = 0;
    public const int CHECKPOINT_COUNT = 19; //includes that starting line


    public UiObject ui;
    public CarObject currentCarDriver;
    public Transform startPoint;
    public GameObject camera;
    public float cameraSmoothing = 5.0f;
    public GameObject simpleCarPrefab;

    private GameController gameController;

    private List<GameObject> carGameObjectList;
    private List<CarObject> carObjectList;
    private GeneticAlgorithm ga;


    // Use this for initialization
    void Start()
    {
        var ga = new GeneticAlgorithm(new TournamentSelection(), new CrossOverSpawnFunction());
        gameController = new GameController(new UnityDebug(), ui, ga, this);
        gameController.Init(GameController.DEFAULT_NUM_CARS);
    }

    // Update is called once per frame
    void Update()
    {
        gameController.Update(Time.deltaTime);
    }

    public int InstantiateCar(ICarBrainController brainController, bool manualControl)
    {
        var carGameObject = Instantiate(simpleCarPrefab, startPoint.position, startPoint.rotation);
        carGameObjectList.Add(carGameObject);
        var carObject = carGameObject.GetComponent<CarObject>();
        carObject.manualControl = manualControl;
        carObject.BrainController = (CarBrainController) brainController;
        brainController.CarObject = carObject;
        carObjectList.Add(carObject);
        return carGameObject.GetInstanceID();
    }

    public void DestroyCarObject(int carObjectId)
    {
        var carObject = carGameObjectList.Find(obj => obj.GetInstanceID() == carObjectId);
        if (carObject != null)
        {
            Destroy(carObject);
            carGameObjectList.Remove(carObject);
        }
    }

    public void ClearCarObjects()
    {
        carGameObjectList = new List<GameObject>();
        carObjectList = new List<CarObject>();
    }

    public void CameraFollowCar(int carObjectId, float deltaTime)
    {
        var carObject = carGameObjectList.Find(c => c.GetInstanceID() == carObjectId);
        var carDriver = carObject.GetComponent<CarObject>();

        camera.transform.position = Vector3.Lerp(camera.transform.position, carDriver.carBody.position,
            deltaTime * cameraSmoothing);
    }


    //factory method
    public ICarBrainController CreateCarBrainController(List<decimal> genomeWeights)
    {
        Genome genome = null;
        if (genomeWeights == null)
        {
            genome = GenomeFactory.CreateRandom();
        }

        genome = GenomeFactory.Create(genomeWeights);
        var neuralNetwork = new NeuralNetwork();
        neuralNetwork.LoadGenome(genome);
        return new CarBrainController(neuralNetwork);
    }
}