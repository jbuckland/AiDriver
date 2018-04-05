using System.Collections.Generic;

namespace Core.GeneticAlgorithm
{
    public interface ISpawnIndividualFunction
    {
        Individual Spawn(List<Individual> parents);
    }
}