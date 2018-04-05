using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Random = System.Random;

public class TournamentSelection : ISelectionFunction
{
    private const int TOUNAMENT_SIZE = 5;
    private const int WINNERS_TO_PICK = 2;
    private Random random;

    public List<Individual> Select(List<Individual> population)
    {
        List<Individual> winningParents = new List<Individual>();

        random = new Random();
        var tournamentSize = PickTournamentSize(population.Count);
        var selectionPool = population.ToArray().ToList();
        for (int i = 0; i < WINNERS_TO_PICK; i++)
        {
            var parent = SelectWinner(selectionPool, tournamentSize);
            selectionPool.Remove(parent);
            winningParents.Add(parent);
        }

        return winningParents;
    }


    private Individual SelectWinner(List<Individual> population, int tournamentSize)
    {
        var selectionPool = population.ToArray().ToList();
        Individual winner = null;

        var selectedIndividuals = new List<Individual>();
        while (selectedIndividuals.Count < tournamentSize)
        {
            var randIndex = random.Next(0, selectionPool.Count - 1);
            selectedIndividuals.Add(selectionPool[randIndex]);
            selectionPool.RemoveAt(randIndex);
        }

        //pick most fit individual
        winner = selectedIndividuals[0];
        foreach (var individual in selectedIndividuals)
        {
            if (individual.Fitness > winner.Fitness)
            {
                winner = individual;
            }
        }


        return winner;
    }


    private int PickTournamentSize(int populationCount)
    {
        var tournamentSize = TOUNAMENT_SIZE;
        if (populationCount >= 2)
        {
            if (populationCount >= TOUNAMENT_SIZE * 2)
            {
                tournamentSize = TOUNAMENT_SIZE;
            }
            else
            {
                tournamentSize = populationCount / 2; //ok with integer division here, we want the floor anyways.
            }
        }
        else
        {
            throw new Exception("need at least 2 in population to do select!");
        }

        return tournamentSize;
    }
}