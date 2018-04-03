using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace Core.Tests
{
    [TestFixture]
    public class NeuralNetworkTests
    {
        [Test]
        public void LoadGenome_Valid_Ok()
        {
            var weights = new List<decimal> {.1m, .2m, .3m, .4m, .5m, .6m, .7m, .8m};
            var genome = new Genome(2, 1, 2, 2, weights);
            var network = new NeuralNetwork();

            Assert.DoesNotThrow(() => { network.LoadGenome(genome); });
        }

        [Test]
        public void CalculateOutputs__()
        {
            var weights = new List<decimal> {.1m, .2m, .3m, .4m, .5m, .6m, .7m, .8m};
            var genome = new Genome(2, 1, 2, 2, weights);
            var network = new NeuralNetwork();
            network.LoadGenome(genome);

            var inputValues = new List<decimal> {1m, 2m};
            var outputValues = network.CalculateOutputs(inputValues);

            Assert.That(outputValues.Count, Is.EqualTo(2));
            Assert.That(outputValues[0], Is.EqualTo(0.91m));
            Assert.That(outputValues[1], Is.EqualTo(1.23m));
        }
    }
}