using UnityEngine;

namespace Modules.Asteroid
{
    public class AsteroidGenerationFactory
    {
        private Sprite sprite;
        
        public AsteroidGenerationFactory(Sprite sprite)
        {
            this.sprite = sprite;
        }
        
        public AsteroidSettings GenerateAsteroidSettings()
        {
            var randomDirection = new Vector2(Random.Range(-1f,1f), Random.Range(-1f,1f));
            var randomVelocity = new Vector2(Random.Range(0f,10f), Random.Range(0f,10f));
            
            return new AsteroidSettings(randomDirection.normalized, randomVelocity.normalized, this.sprite);
        }
    }
}