using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Security.Principal;

public class Neuron
{
    public decimal? OutputValue { get; private set; }
    public string Id { get; set; }


    public List<Synapse> Inputs { get; set; }
    private IInputFunction inputFunction;
    private IOutputFunction outputFunction;


    public Neuron() : this(null, null)
    {
    }


    public Neuron(string id, decimal? value)
    {
        Inputs = new List<Synapse>();
        OutputValue = value;
        Id = id;
    }


    public Neuron(string id) : this(id, null)
    {
    }

    public Neuron(decimal? value) : this(null, value)
    {
    }

    public void SetInputNeuronValue(decimal value)
    {
        OutputValue = value;
    }

    public void AddInputNeuron(Neuron inputNeuron, decimal weight)
    {
        var connection = new Synapse(inputNeuron, weight);
        Inputs.Add(connection);
    }


    //this value needs to be passed on to each of this neuron's output neurons
    public decimal CalculateOutput()
    {
        if (OutputValue == null)
        {
            OutputValue = 0m;

            foreach (var synapse in Inputs)
            {
                if (synapse.InputNeuron != null)
                {
                    //recurse up the tree to get output values.
                    var inputsOutputValue = synapse.InputNeuron.CalculateOutput();
                    OutputValue += inputsOutputValue * synapse.Weight;
                }
                else
                {
                    throw new Exception("input neuron was null!!!");
                }
            }
        }

        return OutputValue.Value;
    }

    public void ClearOutput()
    {
        OutputValue = null;
    }
}