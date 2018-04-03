using System;
using System.Collections.Generic;
using NUnit.Framework;


public class GenomeTests
{
    [Test]
    public void Constructor_NotEnoughWeights_ThrowsException()
    {
        Assert.Throws<Exception>(() =>
        {
            var weights = new List<decimal> {.1m, .2m, .3m, .4m, .5m, .6m, .7m};
            var genome = new Genome(2, 1, 2, 2, weights);
        });
    }

    [Test]
    public void Constructor_CorrectWeights_Ok()
    {
        Assert.DoesNotThrow(() =>
        {
            var weights = new List<decimal> {.1m, .2m, .3m, .4m, .5m, .6m, .7m, .8m};
            var genome = new Genome(2, 1, 2, 2, weights);
        });
    }
}