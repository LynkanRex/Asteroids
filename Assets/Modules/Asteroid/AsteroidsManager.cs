using System;
using System.Collections.Generic;
using Modules.Game;
using UnityEngine;

namespace Modules.Asteroid
{
    public class AsteroidsManager : MonoBehaviour
    {
        // This class delegates and dispatches commands to all classes and systems related to Asteroids and their behaviors
        
        private List<AsteroidController> asteroids;
        private GameState currentGameState;
        
        public Action<GameState> GameStateUpdated;
        public Action<AsteroidController> AsteroidSpawned;

        public void Awake()
        {
            asteroids = new List<AsteroidController>();
            
            this.GameStateUpdated += OnGameStateUpdated;
            this.AsteroidSpawned += OnAsteroidSpawned;
        }
        
        public void Initialize(Action<GameState> gameStateUpdated)
        {
            this.GameStateUpdated = gameStateUpdated;
        }

        private void OnDestroy()
        {
            this.GameStateUpdated -= OnGameStateUpdated;
            this.AsteroidSpawned -= OnAsteroidSpawned;
        }

        private void OnGameStateUpdated(GameState newGameState)
        {
            this.currentGameState = newGameState;
        }

        private void OnAsteroidSpawned(AsteroidController newAsteroid)
        {
            // Tell AsteroidSpawner to spawn an Asteroid and return it
            asteroids.Add(newAsteroid);
        }

        private void FixedUpdate()
        {
            if (currentGameState != GameState.Running) 
                return;
            
            foreach (var asteroidController in asteroids)
            {
                asteroidController.DoMove();
            }
        }
    }
}