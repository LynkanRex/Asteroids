using System;
using Modules.Game;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Modules.Asteroid
{
    internal class AsteroidSpawner : MonoBehaviour, IGameObjectInstanceSpawner
    {
        [SerializeField] public GameObject asteroidPrefab;
        [SerializeField] public Sprite asteroidSprite;

        public Action<AsteroidController> AsteroidControllerSpawned;
        
        public void Initialize()
        {
            var randomAmountOfAsteroidsToSpawnOnStart = Random.Range(4,10);

            for (int i = 0; i < randomAmountOfAsteroidsToSpawnOnStart; i++)
            {
                SpawnInstance();
            }
        }
        
        public void SpawnInstance()
        {
            var asteroidFactory = new AsteroidGenerationFactory(this.asteroidSprite);
            var asteroidSettings = asteroidFactory.GenerateAsteroidSettings();
            
            var instance = Instantiate(this.asteroidPrefab);
            instance.transform.position = new Vector2(Random.Range(-5,5), Random.Range(-5,5));
            
            var asteroidController = instance.GetComponent<AsteroidController>();
            asteroidController.Setup(asteroidSettings);
            
            AsteroidControllerSpawned?.Invoke(asteroidController);
        }
    }
}