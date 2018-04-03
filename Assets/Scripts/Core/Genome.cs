using System;
using System.Collections.Generic;

public class Genome
{
    public int NumInputNeurons { get; private set; }
    public int NumHiddenLayers { get; private set; }
    public int NumNeuronsPerHiddenLayer { get; private set; }
    public int NumOutputNeurons { get; private set; }
    public List<decimal> Weights { get; private set; }


    public Genome(int numInputNeurons, int numHiddenLayers, int numNeuronsPerHiddenLayer, int numOutputNeurons, List<decimal> weights)
    {
        NumInputNeurons = numInputNeurons;
        NumHiddenLayers = numHiddenLayers;
        NumNeuronsPerHiddenLayer = numNeuronsPerHiddenLayer;
        NumOutputNeurons = numOutputNeurons;
        Weights = weights;


        //verify number of weights

        var weightsNeeded = (numInputNeurons * numNeuronsPerHiddenLayer) + (Math.Pow(numNeuronsPerHiddenLayer, 2) * (numHiddenLayers - 1)) +
                            numNeuronsPerHiddenLayer * numOutputNeurons;

        if (weights.Count != weightsNeeded)
        {
            throw new Exception("needed " + weightsNeeded + " weights, but only got " + weights.Count);
        }
    }


    //display this networks genome
    public override string ToString()
    {
        //0: number of input neurons
        //1: number of hidden layers
        //2: number of neurons per hidden layer
        //3: number of outputs
        //4-n: weights

        var genome = "";

        genome += NumInputNeurons.ToString();
        genome += NumHiddenLayers.ToString();
        genome += NumNeuronsPerHiddenLayer.ToString();
        genome += NumOutputNeurons.ToString();

        foreach (var weight in Weights)
        {
            genome += weight;
        }

/*
        foreach (var layer in hiddenLayers)
        {
            foreach (var neuron in layer)
            {
                foreach (var synapse in neuron.Inputs)
                {
                    genome += synapse.Weight;
                }
            }
        }

        foreach (var neuron in OutputNeurons)
        {
            foreach (var synapse in neuron.Inputs)
            {
                genome += synapse.Weight;
            }
        }

*/
        return genome;
    }
}