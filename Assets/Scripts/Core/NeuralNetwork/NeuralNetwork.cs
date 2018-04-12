using System;
using System.Collections.Generic;

public interface INeuralNetwork
{
    List<Neuron> InputNeurons { get; set; }
    List<Neuron> OutputNeurons { get; set; }
    void LoadGenome(Genome genome);
    List<decimal> CalculateOutputs(List<decimal> inputValues);
}

public class NeuralNetwork : INeuralNetwork
{
    public List<Neuron> InputNeurons { get; set; }
    public List<Neuron> OutputNeurons { get; set; }

    private List<List<Neuron>> hiddenLayers;
    private Genome genome;

    public NeuralNetwork()
    {
        InputNeurons = new List<Neuron>();
        hiddenLayers = new List<List<Neuron>>();
        OutputNeurons = new List<Neuron>();
    }

    public void LoadGenome(Genome genome)
    {
        this.genome = genome;
        for (var i = 0; i < genome.NumInputNeurons; i++)
        {
            InputNeurons.Add(new Neuron("input" + i));
        }

        var weightIndex = 0;
        var previousLayer = new List<Neuron>();

        for (var layerId = 0; layerId < genome.NumHiddenLayers; layerId++)
        {
            var hiddenLayer = new List<Neuron>();

            previousLayer = GetPreviousLayer(layerId);
            SetupLayer(hiddenLayer, genome.NumNeuronsPerHiddenLayer, previousLayer, "hl" + layerId + "n", ref weightIndex, genome);

            hiddenLayers.Add(hiddenLayer);
        }


        previousLayer = hiddenLayers[hiddenLayers.Count - 1];
        SetupLayer(OutputNeurons, genome.NumOutputNeurons, previousLayer, "output", ref weightIndex, genome);
    }


    private void SetupLayer(List<Neuron> currentLayer, int neuronCount, List<Neuron> previousLayer, string neuronNamePrefix, ref int weightIndex, Genome genome)
    {
        for (var neuronId = 0; neuronId < neuronCount; neuronId++)
        {
            var neuron = new Neuron(neuronNamePrefix + neuronId);

            AddSynapsesToNeuron(neuron, previousLayer, ref weightIndex, genome);
            currentLayer.Add(neuron);
        }
    }


    private void AddSynapsesToNeuron(Neuron neuron, List<Neuron> previousLayer, ref int genomeIndex, Genome genome)
    {
        foreach (var previousNeuron in previousLayer)
        {
            var nextWeight = genome.Weights[genomeIndex];
            genomeIndex++;
            var synapse = new Synapse(previousNeuron, nextWeight);
            neuron.Inputs.Add(synapse);
        }
    }

    private List<Neuron> GetPreviousLayer(int currentLayerIndex)
    {
        var previousLayer = InputNeurons;
        if (currentLayerIndex != 0)
        {
            previousLayer = hiddenLayers[currentLayerIndex - 1];
        }

        return previousLayer;
    }

    public List<decimal> CalculateOutputs(List<decimal> inputValues)
    {
        var outputs = new List<decimal>();

        if (inputValues.Count != InputNeurons.Count)
        {
            throw new Exception("not enough input values for all input neurons! Needed " + InputNeurons.Count + " but got " + inputValues.Count);
        }

        //reset the values from all neurons
        for (int i = 0; i < InputNeurons.Count; i++)
        {
            InputNeurons[i].SetInputNeuronValue(inputValues[i]);
        }

        foreach (var layer in hiddenLayers)
        {
            foreach (var neuron in layer)
            {
                neuron.ClearOutput();
            }
        }

        foreach (var neuron in OutputNeurons)
        {
            neuron.ClearOutput();
        }


        foreach (var outputNeuron in OutputNeurons)
        {
            outputs.Add(outputNeuron.CalculateOutput());
        }

        return outputs;
    }
}