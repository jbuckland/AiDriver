using System;
using System.Collections.Generic;
using UnityEngine;

public class TournamentSelection : ISelectionFunction
{
    private const int TOUNAMENT_COUNT = 5;

    public List<Individual> Select(List<Individual> population)
    {
        var numToPick = TOUNAMENT_COUNT;
        if (population.Count >= 2)
        {
            if (population.Count >= TOUNAMENT_COUNT * 2)
            {
            }
            else
            {
                numToPick = population.Count / 2; //ok with integer division here, we want the floor anyways.
            }
        }
        else
        {
            throw new Exception("need at least 2 in population to select!");
        }
        
        
        
        
        
    }
}