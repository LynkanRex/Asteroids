using System;
using Modules.Interfaces;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Modules.Asteroid
{
    public class AsteroidController : MonoBehaviour, IController, IDamageable, IDestructible
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        private AsteroidSettings settings;
        private Rigidbody2D rb2d;
        
        private float maxVelocityMagnitude = 3;
        
        public void Setup(AsteroidSettings settings)
        {
            this.settings = settings;
            
            this.spriteRenderer.sprite = settings.Sprite;
            this.rb2d = gameObject.AddComponent<Rigidbody2D>();
            this.rb2d.gravityScale = 0;
        }
        
        public void DoMove()
        {
            this.rb2d.AddForce(settings.Direction * settings.Velocity, ForceMode2D.Force);

            if (rb2d.velocity.sqrMagnitude >= maxVelocityMagnitude)
                this.rb2d.velocity = this.rb2d.velocity.normalized * maxVelocityMagnitude;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            // TODO: If other Asteroid, or if Player, don't TakeDamage

            if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("projectile"))
                return;

            TakeDamage();
        }

        public void TakeDamage()
        {
            this.Destruct();
        }

        public void Destruct()
        {
            // TODO: Particle Effect
            // TODO: Sound Effect

            Addressables.ReleaseInstance(this.gameObject);
            
            Destroy(this.gameObject);
        }
    }

    [Serializable]
    public class AsteroidSettings
    {
        private readonly Sprite sprite;
        
        private readonly Vector2 direction;
        private readonly Vector2 velocity;

        public Sprite Sprite => this.sprite;
        
        public Vector2 Direction => this.direction;
        public Vector2 Velocity => this.velocity;

        public AsteroidSettings(Vector2 direction, Vector2 velocity, Sprite sprite)
        {
            this.sprite = sprite;
            this.direction = direction;
            this.velocity = velocity;
        }
    }
}