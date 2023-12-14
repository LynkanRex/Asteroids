using System.Collections.Generic;
using UnityEngine;

namespace Modules.Asteroid
{
    public class AsteroidsManager : MonoBehaviour
    {
        private readonly List<AsteroidController> asteroids;

        public AsteroidsManager(List<AsteroidController> asteroids)
        {
            this.asteroids = asteroids;
        }

        private void FixedUpdate()
        {
            foreach (var asteroidController in asteroids)
            {
                asteroidController.DoMove();
            }
        }
    }
}