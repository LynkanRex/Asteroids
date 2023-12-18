using System;
using Modules.Asteroid;
using Modules.Interfaces;
using UnityEngine;

namespace Modules.Player
{
    public class PlayerController : MonoBehaviour, IController, IDamageable, IDestructible
    {
        [SerializeField] private PlayerSettings settings;
        private Vector3 moveDir;
        
        public Action PlayerDeath;

        public void Initialize()
        {
            this.transform.position = Vector3.zero;
            this.transform.rotation = Quaternion.identity;
        }
        
        private void Update()
        {
            var verticalInput = Input.GetAxis("Vertical");
            moveDir = transform.up * verticalInput;
        }

        private void FixedUpdate()
        {
            this.gameObject.transform.Rotate(new Vector3(0,0,-Input.GetAxis("Horizontal") * settings.rotateSpeed));
            this.settings.rb2d.MovePosition(transform.position + moveDir * (settings.moveSpeed * Time.deltaTime));
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.CompareTag("asteroid"))
                return;
            
            TakeDamage();
        }

        public void TakeDamage()
        {
            Destruct();
        }

        public void Destruct()
        {
            PlayerDeath?.Invoke();
            this.gameObject.SetActive(false);
        }
    }

    [Serializable]
    public class PlayerSettings
    {
        [SerializeField] public Rigidbody2D rb2d;
        
        [SerializeField] public float moveSpeed;
        [SerializeField] public float rotateSpeed;
    }

    public class PlayerShooting : MonoBehaviour
    {
        [SerializeField] private GameObject projectilePrefab;

        public bool isAlive;
        private bool canShoot;
        
        private float shotCooldown;
        private float remainingCooldownTime;
        
        private void Update()
        {
            if (isAlive && canShoot && Input.GetKeyDown(KeyCode.Space))
            {
                Shoot();
                remainingCooldownTime += shotCooldown;
                canShoot = false;
                return;
            }

            if (canShoot) return;
            
            remainingCooldownTime -= Time.deltaTime;
            remainingCooldownTime = Mathf.Clamp(remainingCooldownTime, 0,shotCooldown);
            
            canShoot = true;
        }

        private void Shoot()
        {
            var instance = Instantiate(projectilePrefab, this.transform.position, this.transform.rotation);
        }
    }
}
