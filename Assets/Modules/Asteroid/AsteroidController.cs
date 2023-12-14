using Modules.Interfaces;
using UnityEngine;

namespace Modules.Asteroid
{
    public class AsteroidController : MonoBehaviour, IController, IDamageable
    {
        private readonly Vector2 direction;
        private readonly Vector2 velocity;

        private readonly Rigidbody2D rb2d;

        public AsteroidController(Vector2 direction, Vector2 velocity)
        {
            this.direction = direction;
            this.velocity = velocity;
            
            rb2d = this.gameObject.AddComponent<Rigidbody2D>();
        }
        
        public void DoMove()
        {
            rb2d.AddForce(direction * velocity, ForceMode2D.Force);
        }
        
        public void TakeDamage()
        {
            throw new System.NotImplementedException();
        }
    }
}