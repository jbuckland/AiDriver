public class Synapse
{
    public Neuron InputNeuron { get; set; }

    public Synapse(Neuron inputNeuron)
    {
        InputNeuron = inputNeuron;
        Weight = 1m;
    }

    public Synapse(Neuron inputNeuron, decimal weight)
    {
        InputNeuron = inputNeuron;
        Weight = weight;
    }


    public decimal Weight { get; private set; }
}