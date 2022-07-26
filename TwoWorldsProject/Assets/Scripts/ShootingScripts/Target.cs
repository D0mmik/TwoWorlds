using UnityEngine;

namespace ShootingScripts
{
    public class Target : MonoBehaviour
    {
        float health = 50f;
        public void TakeDamage(float amount)
        {
            health -= amount;
            if(health <= 0f)
                Die();
        
            void Die()
            {
                Destroy(this.gameObject);
            }
        }

    }
}
