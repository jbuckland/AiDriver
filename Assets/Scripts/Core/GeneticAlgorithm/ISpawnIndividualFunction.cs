using System.Collections.Generic;

public interface ISpawnIndividualFunction
{
    Individual Spawn(List<Individual> parents);
}