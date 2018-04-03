using NUnit.Framework;

namespace Core.Tests
{
    [TestFixture]
    public class NeuronTests
    {
        [Test]
        public void CalculateOutput_HasAllInputs_CalculatesAndStoresValue()
        {
            var neuron = new Neuron();
            neuron.AddInputNeuron(new Neuron(14), .5m);
            neuron.AddInputNeuron(new Neuron(29), .25m);
            neuron.AddInputNeuron(new Neuron(4), -.75m);


            Assert.That(neuron.CalculateOutput(), Is.EqualTo(11.25m));

            Assert.That(neuron.CalculateOutput(), Is.EqualTo(11.25m));
        }

        [Test]
        public void CalculateOutput_MultiLayer_CalculatesAndStoresValue()
        {
            var inputNeuron1 = new Neuron("inputNeuron1", 1);
            var inputNeuron2 = new Neuron("inputNeuron2", 2);

            var layer1Neuron1 = new Neuron("layer1Neuron1");
            var layer1Neuron2 = new Neuron("layer1Neuron2");

            var outNeuron1 = new Neuron("outNeuron1");
            var outNeuron2 = new Neuron("outNeuron2");


            layer1Neuron1.AddInputNeuron(inputNeuron1, .1m);
            layer1Neuron1.AddInputNeuron(inputNeuron2, .2m);

            layer1Neuron2.AddInputNeuron(inputNeuron1, .3m);
            layer1Neuron2.AddInputNeuron(inputNeuron2, .4m);

            outNeuron1.AddInputNeuron(layer1Neuron1, .5m);
            outNeuron1.AddInputNeuron(layer1Neuron2, .6m);

            outNeuron2.AddInputNeuron(layer1Neuron1, .7m);
            outNeuron2.AddInputNeuron(layer1Neuron2, .8m);


            Assert.That(outNeuron1.CalculateOutput(), Is.EqualTo(0.91m));
            Assert.That(outNeuron2.CalculateOutput(), Is.EqualTo(1.23m));
        }
    }
}