using System.Collections.Generic;

public interface ISelectionFunction
{
    List<Individual> Select(List<Individual> population);
}