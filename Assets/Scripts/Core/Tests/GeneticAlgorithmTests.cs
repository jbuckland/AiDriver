using System;
using System.Collections.Generic;
using NUnit.Framework;


public class GeneticAlgorithmTests
{
    private GeneticAlgorithm ga;
    private ISelectionFunction tournamentSel;
    private ISpawnIndividualFunction crossOver;

    [SetUp]
    public void Setup()
    {
        tournamentSel = new TournamentSelection();
        crossOver = new CrossOverSpawnFunction();

        ga = new GeneticAlgorithm(tournamentSel, crossOver);
    }

    [Test]
    public void GeneticAlgorithmMakeNewGeneration__()
    {
        var population = new List<Individual>();
        population.Add(new Individual {Fitness = 1, Genome = new Genome(3, 1, 2, 2, new List<decimal> {1, 1, 1, 1, 1, 1, 1, 1, 1, 1})});
        population.Add(new Individual {Fitness = 2, Genome = new Genome(3, 1, 2, 2, new List<decimal> {2, 2, 2, 2, 2, 2, 2, 2, 2, 2})});
        population.Add(new Individual {Fitness = 3, Genome = new Genome(3, 1, 2, 2, new List<decimal> {3, 3, 3, 3, 3, 3, 3, 3, 3, 3})});
        population.Add(new Individual {Fitness = 4, Genome = new Genome(3, 1, 2, 2, new List<decimal> {4, 4, 4, 4, 4, 4, 4, 4, 4, 4})});
        population.Add(new Individual {Fitness = 5, Genome = new Genome(3, 1, 2, 2, new List<decimal> {5, 5, 5, 5, 5, 5, 5, 5, 5, 5})});
        population.Add(new Individual {Fitness = 6, Genome = new Genome(3, 1, 2, 2, new List<decimal> {6, 6, 6, 6, 6, 6, 6, 6, 6, 6})});
        population.Add(new Individual {Fitness = 7, Genome = new Genome(3, 1, 2, 2, new List<decimal> {7, 7, 7, 7, 7, 7, 7, 7, 7, 7})});
        population.Add(new Individual {Fitness = 8, Genome = new Genome(3, 1, 2, 2, new List<decimal> {8, 8, 8, 8, 8, 8, 8, 8, 8, 8})});
        population.Add(new Individual {Fitness = 9, Genome = new Genome(3, 1, 2, 2, new List<decimal> {9, 9, 9, 9, 9, 9, 9, 9, 9, 9})});
        population.Add(new Individual {Fitness = 10, Genome = new Genome(3, 1, 2, 2, new List<decimal> {10, 10, 10, 10, 10, 10, 10, 10, 10, 10})});


        var newGeneration = ga.MakeNewGeneration(population, 10);

        Assert.That(newGeneration, Is.Not.Null);
        Assert.That(newGeneration.Count, Is.EqualTo(10));
    }


    [Test]
    public void CrossOverSpawn__()
    {
        var parents = new List<Individual>();
        var p1Genome = new Genome(3, 1, 2, 2, new List<decimal> {1, 3, 5, 7, 9, 11, 13, 15, 17, 19});
        parents.Add(new Individual {Genome = p1Genome});

        var p2Genome = new Genome(3, 1, 2, 2, new List<decimal> {2, 4, 6, 8, 10, 12, 14, 16, 18, 20});
        parents.Add(new Individual {Genome = p2Genome});


        var baby = crossOver.Spawn(parents);

        Assert.That(baby, Is.Not.Null);
        Assert.That(baby.Genome.NumInputNeurons, Is.EqualTo(3));
        Assert.That(baby.Genome.NumHiddenLayers, Is.EqualTo(1));
        Assert.That(baby.Genome.NumNeuronsPerHiddenLayer, Is.EqualTo(2));
        Assert.That(baby.Genome.NumOutputNeurons, Is.EqualTo(2));
    }


    [Test]
    public void TournamentSelectionSelect_OnlyTwo_SelectsBoth()
    {
        var population = new List<Individual>();
        population.Add(new Individual {Fitness = 1, Id = "1"});
        population.Add(new Individual {Fitness = 2, Id = "2"});


        var winners = tournamentSel.Select(population);

        Assert.That(winners, Is.Not.Null);
        Assert.That(winners.Count, Is.EqualTo(2));
        Assert.That(winners[0].Id, Is.Not.EqualTo(winners[1].Id));
    }


    [Test]
    public void TournamentSelectionSelect_Many_SelectsStuff()
    {
        var population = new List<Individual>();
        population.Add(new Individual {Fitness = 1, Id = "1"});
        population.Add(new Individual {Fitness = 2, Id = "2"});
        population.Add(new Individual {Fitness = 3, Id = "3"});
        population.Add(new Individual {Fitness = 4, Id = "4"});
        population.Add(new Individual {Fitness = 5, Id = "5"});
        population.Add(new Individual {Fitness = 6, Id = "6"});
        population.Add(new Individual {Fitness = 7, Id = "7"});
        population.Add(new Individual {Fitness = 8, Id = "8"});
        population.Add(new Individual {Fitness = 9, Id = "9"});
        population.Add(new Individual {Fitness = 10, Id = "10"});
        population.Add(new Individual {Fitness = 11, Id = "11"});
        population.Add(new Individual {Fitness = 12, Id = "12"});
        population.Add(new Individual {Fitness = 13, Id = "13"});
        population.Add(new Individual {Fitness = 14, Id = "14"});
        population.Add(new Individual {Fitness = 15, Id = "15"});
        population.Add(new Individual {Fitness = 16, Id = "16"});
        population.Add(new Individual {Fitness = 17, Id = "17"});
        population.Add(new Individual {Fitness = 18, Id = "18"});
        population.Add(new Individual {Fitness = 19, Id = "19"});
        population.Add(new Individual {Fitness = 20, Id = "20"});


        var winners = tournamentSel.Select(population);

        Assert.That(winners, Is.Not.Null);
        Assert.That(winners.Count, Is.EqualTo(2));
        Assert.That(winners[0].Id, Is.Not.EqualTo(winners[1].Id));
    }
}