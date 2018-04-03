
using System.Collections.Generic;

public class Neuron
{
    public List<Synapse> Inputs { get; set; }
    public List<Synapse> Outputs { get; set; }

    private IInputFunction inputFunction;
    private IOutputFunction outputFunction;

    public Neuron()
    {
        Inputs = new List<Synapse>();
        Outputs = new List<Synapse>();
    }

    public void AddInputNeuron(Neuron inputNeuron)
    {
        var connection = new Synapse(inputNeuron, this);
        Inputs.Add(connection);
        inputNeuron.Outputs.Add(connection);
    }

    public void AddOutputNeuron(Neuron outputNeuron)
    {
        var connection = new Synapse(this, outputNeuron);
        Outputs.Add(connection);
        outputNeuron.Inputs.Add(connection);

    }

    //this value needs to be passed on to each of this neuron's output neurons
    public decimal CalculateOutput()
    {
        var inputValue = inputFunction.CalculateInput(Inputs);
        var outputVal = outputFunction.CalculateOutput(inputValue);

        return outputVal;
    }

}
