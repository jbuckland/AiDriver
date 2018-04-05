using System;
using System.Collections.Generic;
using NUnit.Framework;


public class GeneticAlgorithmTests
{
    private GeneticAlgorithm ga;


    [SetUp]
    public void Setup()
    {
        ga = new GeneticAlgorithm();
    }

    [Test]
    public void Select()
    {
        Individual father = null;
        Individual mother = null;

        var population = new List<Individual>();
        population.Add(new Individual {Id = "111", Score = 100m});
        population.Add(new Individual {Id = "222", Score = 1m});


        ga.SelectParents(population, father, mother);

        Assert.That(father, Is.Not.Null);
        Assert.That(mother, Is.Not.Null);
        Assert.That(father.Id, Is.Not.EqualTo(mother.Id));
    }
}