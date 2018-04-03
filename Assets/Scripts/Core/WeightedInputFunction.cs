using System.Collections.Generic;

public class WeightedInputFunction : IInputFunction
{
    public decimal CalculateInput(List<Synapse> inputs)
    {
        decimal inputVal = 0;

        foreach(Synapse syn in inputs)
        {
            inputVal += syn. * syn.Weight;
        }

        return inputVal;
    }
}
