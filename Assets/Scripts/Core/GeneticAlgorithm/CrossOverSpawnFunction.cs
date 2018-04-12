using System;
using System.Collections.Generic;

public class CrossOverSpawnFunction : ISpawnIndividualFunction
{
    public Individual Spawn(List<Individual> parents)
    {
        if (parents == null) throw new Exception("parents were null");
        if (parents.Count != 2) throw new Exception("expected 2 parents, got " + parents.Count);

        var baby = new Individual();

        var fatherGenome = parents[0].Genome;
        var fatherWeights = fatherGenome.Weights;
        var motherWeights = parents[1].Genome.Weights;


        var crossOverIndex = new Random().Next(0, fatherWeights.Count - 1);


        var babyWeights = new List<decimal>();


        for (int i = 0; i < fatherWeights.Count; i++)
        {
            if (i < crossOverIndex)
            {
                babyWeights.Add(fatherWeights[i]);
            }
            else
            {
                babyWeights.Add(motherWeights[i]);
            }
        }

        baby.Genome = new Genome(fatherGenome.NumInputNeurons,
            fatherGenome.NumHiddenLayers,
            fatherGenome.NumNeuronsPerHiddenLayer,
            fatherGenome.NumOutputNeurons,
            babyWeights);


        return baby;
    }
}