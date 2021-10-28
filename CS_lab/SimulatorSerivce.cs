﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CS_lab.FoodGenerator;
using CS_lab.GameStateWriter;
using CS_lab.Models;
using CS_lab.NameGenerator;
using CS_lab.WormStrategy;
using Microsoft.Extensions.Hosting;
using static System.Threading.Tasks.Task;

namespace CS_lab
{
    public class SimulatorService : IHostedService
    {
        private const int GameSteps = 100;

        private readonly World _world;
        private readonly List<Worm> _worms;
        private readonly List<Food> _foods;
        
        private readonly IHostApplicationLifetime _appLifetime;
        private readonly IFoodGenerator _foodGenerator;
        private readonly INameGenerator _nameGenerator;
        private readonly IWormStrategy _wormStrategy;
        private readonly IGameStateWriter _gameStateWriter;

        private int _gameStep;

        public SimulatorService(
            IHostApplicationLifetime appLifetime,
            IFoodGenerator foodGenerator,
            INameGenerator nameGenerator,
            IWormStrategy wormStrategy,
            IGameStateWriter gameStateWriter)
        {
            _appLifetime = appLifetime;
            _foodGenerator = foodGenerator;
            _nameGenerator = nameGenerator;
            _wormStrategy = wormStrategy;
            _gameStateWriter = gameStateWriter;

            _worms = new List<Worm>();
            _foods = new List<Food>();
            _world = new World(_worms, _foods);
        }

        public void StartGame()
        {
            InitWorms();
            PrintLog();
            for (var i = 0; i < GameSteps; i++)
            {
                _gameStep = i;
                GenerateFood();
                CheckWormsFoods();
                AskWorms();
                CheckWormsFoods();
                RemoveBadFoods();
                RemoveBadWorms();
                PrintLog();
            }
        }

        private void GenerateFood()
        {
            var food = _foodGenerator.GenerateFood(_world);
            _world.AddFood(food);
        }

        private void CheckWormsFoods()
        {
            _world.CheckWormsFoods();
        }

        private void RemoveBadFoods()
        {
            foreach (var food in _foods)
            {
                food.DecreaseHealth();
            }

            _foods.RemoveAll(food => food.Health <= 0);
        }

        private void RemoveBadWorms()
        {
            _worms.RemoveAll(worm => worm.Health <= 0);
        }

        private void PrintLog()
        {
            _gameStateWriter.WriteGameState(_world);
        }

        private void InitWorms()
        {
            var startPosition = new Position(0, 0);
            var worm = new Worm(_nameGenerator.NextName(), startPosition, _wormStrategy);
            _world.AddWorm(worm);
        }

        private void AskWorms()
        {
            foreach (var worm in _worms)
            {
                var (nextStep, nextDirection) = worm.NextStep(_world, _gameStep);
                var newPosition = worm.Position.DirectedPosition(nextDirection);

                switch (nextStep)
                {
                    case WormStep.Move:
                        if (!_world.IsWormOnPosition(newPosition))
                        {
                            worm.Position = newPosition;
                        }

                        break;
                    case WormStep.Spawn:
                        if (!_world.IsWormOnPosition(newPosition) && !_world.IsFoodOnPosition(newPosition) &&
                            worm.EnableToSpawn())
                        {
                            var newWorm = new Worm(_nameGenerator.NextName(), newPosition, _wormStrategy);
                            _world.AddWorm(newWorm);
                        }

                        break;
                    case WormStep.Nothing:
                        break;
                }

                worm.DecreaseHealth();
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _appLifetime.ApplicationStarted.Register(() =>
            {
                Run( () =>
                {
                    try
                    {
                        StartGame();
                    }
                    finally
                    {
                        _appLifetime.StopApplication();
                    }
                }, cancellationToken);
            });
            return CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return CompletedTask;
        }
    }
}