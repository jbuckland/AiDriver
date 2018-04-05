using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine.Experimental.Rendering;


public class GeneticAlgorithm
{
    private Random rand;
    private ISelectionFunction selectionFunction;

    public GeneticAlgorithm(ISelectionFunction selectionFunction)
    {
        this.selectionFunction = selectionFunction;
        rand = new Random();
    }


    public List<Individual> MakeNewGeneration(List<Individual> population, int numInNewPopulation)
    {
        population.Sort((a, b) => a.Score.CompareTo(b.Score));


        var newPopulation = new List<Individual>();

        for (int i = 0; i < numInNewPopulation; i++)
        {
            Individual father = new Individual();
            Individual mother = new Individual();
            var parents = selectionFunction.Select(population);
            if (parents == null || parents.Count != 2)
            {
                throw new Exception("parents were null, or not 2 of them!");
            }

            var baby = Mate(parents[0], parents[1]);
            newPopulation.Add(baby);
        }

        return newPopulation;
    }

    public void SelectParents(List<Individual> population, Individual father, Individual mother)
    {
        var totalScore = 0m;
        population.ForEach(i => { totalScore += i.Score; });
        var randScore = (decimal) rand.NextDouble() * totalScore;

        var currentScore = totalScore;
        foreach (var individual in population)
        {
            currentScore -= individual.Score;
            if (randScore > currentScore)
            {
                father = individual;
                break;
            }
        }
    }


    public void TournamentSelection(List<Individual> population, Individual father, Individual mother)
    {
    }


    public void RankSelection(List<Individual> population, Individual father, Individual mother)
    {
    }


    public Individual Mate(Individual father, Individual mother)
    {
        var baby = new Individual();


        return baby;
    }
}