
public class Synapse
{
    private Neuron inputNeuron;
    private Neuron outputNeuron;

    public Synapse(Neuron inputNeuron, Neuron outputNeuron)
    {
        this.inputNeuron = inputNeuron;
        this.outputNeuron = outputNeuron;
    }

    public decimal GetOutputValue()
    {
        return outputNeuron.CalculateOutput();
    }

    public decimal Weight { get; internal set; }
}
