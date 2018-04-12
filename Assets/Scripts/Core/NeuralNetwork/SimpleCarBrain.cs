using System.Collections.Generic;

public class SimpleCarBrain
{
    public decimal Speed { get; private set; }
    public decimal Turn { get; private set; }
    public Genome Genome { get; set; }

    private NeuralNetwork neuralNetwork;

    public SimpleCarBrain(List<decimal> weights)
    {
        neuralNetwork = new NeuralNetwork();

        var numInputNeurons = 6;
        var numHiddenLayers = 2;
        var numNeuronsPerHiddenLayer = 8;
        var numOutputNeurons = 4;

        Genome = null;
        if (weights != null)
        {
            Genome = new Genome(numInputNeurons, numHiddenLayers, numNeuronsPerHiddenLayer, numOutputNeurons,
                weights);
        }
        else
        {
            Genome = new Genome(numInputNeurons, numHiddenLayers, numNeuronsPerHiddenLayer, numOutputNeurons);
        }

        neuralNetwork.LoadGenome(Genome);
    }

    public SimpleCarBrain() : this(null)
    {
    }


    public void Evaluate(decimal frontDist, decimal frontLeftDist, decimal frontRightDist, decimal leftdist,
        decimal rightDist, decimal speed)
    {
        var outputs = neuralNetwork.CalculateOutputs(new List<decimal>
        {
            frontDist,
            frontLeftDist,
            frontRightDist,
            leftdist,
            rightDist,
            speed
        });
        var speedUp = outputs[0];
        var speedDown = outputs[1];
        var turnLeft = outputs[2];
        var turnRight = outputs[3];

        Speed = 0;
        if (speedUp + speedDown != 0)
        {
            Speed = (speedUp - speedDown) / (speedUp + speedDown);
        }

        Turn = 0;
        if (turnLeft + turnRight != 0)
        {
            Turn = (turnRight - turnLeft) / (turnRight + turnLeft);
        }
    }
}