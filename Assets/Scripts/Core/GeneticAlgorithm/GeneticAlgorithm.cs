using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Core.GeneticAlgorithm;
using UnityEngine.Experimental.Rendering;


public class GeneticAlgorithm
{
    private Random rand;
    private ISelectionFunction selectionFunction;
    private ISpawnIndividualFunction spawnFunction;
    private double MUTATION_RATE = .1;
    private double MAX_MUTATION_SEVERITY = .50;

    public GeneticAlgorithm(ISelectionFunction selectionFunction, ISpawnIndividualFunction spawnFunction)
    {
        this.selectionFunction = selectionFunction;
        this.spawnFunction = spawnFunction;
        rand = new Random();
    }


    public List<Individual> MakeNewGeneration(List<Individual> population, int numInNewPopulation)
    {
        population.Sort((a, b) => a.Fitness.CompareTo(b.Fitness));


        var newPopulation = new List<Individual>();

        for (int i = 0; i < numInNewPopulation; i++)
        {
            var parents = selectionFunction.Select(population);
            if (parents == null || parents.Count != 2)
            {
                throw new Exception("parents were null, or not 2 of them!");
            }


            var baby = spawnFunction.Spawn(parents);

            //do mutation?
            var doMutation = rand.NextDouble();
            if (doMutation.CompareTo(MUTATION_RATE) < 0)
            {
                var mutationSeverity = rand.NextDouble() * MAX_MUTATION_SEVERITY;
                Mutate(baby, mutationSeverity);
            }


            newPopulation.Add(baby);
        }

        return newPopulation;
    }

    private void Mutate(Individual individual, double mutationSeverity)
    {
        //for each weight, there is a mutationSeverity change that we generate a new random weight.

        for (int i = 0; i < individual.Genome.Weights.Count; i++)
        {
            var mutationRoll = rand.NextDouble();
            if (mutationRoll.CompareTo(mutationSeverity) < 0)
            {
                individual.Genome.Weights[i] = Genome.GetRandomWeight();
            }
        }
    }
}