using System;
using PlayerScripts;
using UnityEngine;

namespace ShootingScripts
{
    public class Target : MonoBehaviour
    {
        [SerializeField] float Health = 50f;
        GameObject player;
        FPSController fpsController;

        void Awake()
        {
             fpsController = GameObject.FindGameObjectWithTag("Player")?.GetComponent<FPSController>();
        }

        public void TakeDamage(float amount)
        {
            Health -= amount;
            if(Health <= 0f)
                Die();
        
            void Die()
            {
                fpsController.KillCount++;
                Destroy(this.gameObject);
            }
        }

    }
}
