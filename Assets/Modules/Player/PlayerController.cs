using Modules.Interfaces;
using UnityEngine;

namespace Modules.Player
{
    public class PlayerController : MonoBehaviour, IController, IDamageable
    {
        public void TakeDamage()
        {
            throw new System.NotImplementedException();
        }
    }
}
