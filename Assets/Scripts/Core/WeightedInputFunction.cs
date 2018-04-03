using System;
using System.Collections.Generic;

public class WeightedInputFunction : IInputFunction
{
    public decimal CalculateInput(List<Synapse> inputs)
    {
        decimal inputVal = 0;
/*
        foreach (Synapse syn in inputs)
        {
            if (syn.InputNeuron != null && syn.InputNeuron.Value != null)
            {
                inputVal += syn.InputNeuron.Value.Value * syn.Weight;
            }
            else
            {
                throw new Exception("input neuron was missing a value!!!");
            }
        }
*/
        return inputVal;
    }
}