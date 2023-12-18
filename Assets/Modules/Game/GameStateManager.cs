using System;
using Modules.Asteroid;
using Modules.Menu;
using Modules.Player;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Modules.Game
{
    public class GameStateManager : MonoBehaviour
    {
        // This Manager controls all the moving parts of the application and listens to/dispatches actions depending on
        // the context in which actions occur, such as New Game
        
        private IGameObjectInstanceSpawner asteroidSpawner;
        [SerializeField] private AsteroidsManager asteroidsManager;
        [SerializeField] private GameMenu gameMenu;
        [SerializeField] private PlayerController playerController;
        
        private GameState gameState = GameState.Stopped;

        public Action<GameState> UpdateGameState;
        
        private void Awake()
        {
            this.asteroidSpawner = GetComponent<AsteroidSpawner>();
            
            UpdateGameState += OnUpdateGameState;
            
            gameMenu.MenuButtonClicked += OnMenuButtonClicked;
            playerController.PlayerDeath += OnPlayerDeath;

            var asAsteroidSpawner = (AsteroidSpawner)asteroidSpawner;
            asAsteroidSpawner.AsteroidControllerSpawned += AsteroidControllerSpawned;
        }
        
        private void OnDestroy()
        {
            UpdateGameState -= OnUpdateGameState;
            
            gameMenu.MenuButtonClicked -= OnMenuButtonClicked;
            playerController.PlayerDeath -= OnPlayerDeath;
            
            var asAsteroidSpawner = (AsteroidSpawner)asteroidSpawner;
            asAsteroidSpawner.AsteroidControllerSpawned -= AsteroidControllerSpawned;
        }
        
        private void AsteroidControllerSpawned(AsteroidController asteroidController)
        {
            asteroidsManager.AsteroidSpawned?.Invoke(asteroidController);
        }

        private void OnMenuButtonClicked(GameMenu.MenuActionType menuAction)
        {
            switch (menuAction)
            {
                case GameMenu.MenuActionType.NewGame:
                    playerController.Initialize();
                    asteroidsManager.Initialize(UpdateGameState);
                    asteroidSpawner.Initialize();
                    break;
                default:
#if UNITY_EDITOR
                    Debug.Log("GameStateManager.OnMenuButtonClicked: Quitting the game, Reason: Default state was reached");
                    EditorApplication.isPlaying = false;
                    break;
#else
                    Application.Quit();
                    break;
#endif
            }
        }

        private void OnPlayerDeath()
        {
            this.gameState = GameState.Stopped;
            
            UpdateGameState?.Invoke(this.gameState);
            gameMenu.ShowMenu(true);
        }
        
        private void OnUpdateGameState(GameState newGameState)
        {
            this.gameState = newGameState;

            switch (this.gameState)
            {
                case GameState.Running:
                    break;
                case GameState.Stopped:
                    break;
                default:
                    break;
            }
        }
    }
}