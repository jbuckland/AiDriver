using NSubstitute;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using NUnit.Framework;

namespace Core.Tests
{
    public class GameControllerTests
    {
        private GameController controller;
        private IMainGameObject mainGameObject;
        private ICarBrainController brain1;
        private ICarBrainController brain2;
        private ICarBrainController brain3;
        private ICarBrainController brain4;
        private ICarBrainController brain5;
        private IGeneticAlgorithm ga;
        private IDebug debug;

        [SetUp]
        public void Setup()
        {
            var uiObject = Substitute.For<IUiObject>();
            ga = Substitute.For<IGeneticAlgorithm>();
            debug = Substitute.For<IDebug>();


            brain1 = Substitute.For<ICarBrainController>();
            brain2 = Substitute.For<ICarBrainController>();
            brain3 = Substitute.For<ICarBrainController>();
            brain4 = Substitute.For<ICarBrainController>();
            brain5 = Substitute.For<ICarBrainController>();

            mainGameObject = Substitute.For<IMainGameObject>();
            mainGameObject.CreateCarBrainController(Arg.Any<List<decimal>>()).Returns(brain1, brain2, brain3, brain4, brain5);

            controller = new GameController(debug, uiObject, ga, mainGameObject);

            controller.Init(5);
        }


        [Test]
        public void Init_Valid_CreateBrainsWithNullWeights()
        {
            ga.Received(0).MakeNewGeneration(Arg.Any<List<Individual>>(), Arg.Any<int>());
            mainGameObject.Received(5).CreateCarBrainController(null);
            mainGameObject.Received(0).DestroyCarObject(Arg.Any<int>()); //no car objects to destroy yet
            mainGameObject.Received(5).InstantiateCar(Arg.Any<ICarBrainController>(), Arg.Any<bool>());
        }

        [Test]
        public void Update_AllCarsDead_stuff()
        {
            brain1.IsAlive = false;
            brain2.IsAlive = false;
            brain3.IsAlive = false;
            brain4.IsAlive = false;
            brain5.IsAlive = false;
            List<Individual> newPopulation = new List<Individual>();
            newPopulation.Add(new Individual {Id = "1"});
            newPopulation.Add(new Individual {Id = "2"});
            newPopulation.Add(new Individual {Id = "3"});
            newPopulation.Add(new Individual {Id = "4"});
            newPopulation.Add(new Individual {Id = "5"});
            ga.MakeNewGeneration(Arg.Any<List<Individual>>(), Arg.Any<int>()).Returns(newPopulation);


            controller.Update(1f);

            ga.Received(1).MakeNewGeneration(Arg.Any<List<Individual>>(), Arg.Any<int>());
        }
    }
}