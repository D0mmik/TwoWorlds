using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{
    public class EnemiesSpawner : MonoBehaviour
    {
        [SerializeField] GameObject Enemy1;
        [SerializeField] float TimeToSpawn;
        
        [Header("SpawnPositions")] 
        [SerializeField] float MinX;
        [SerializeField] float MaxX;
        [SerializeField] float MinZ;
        [SerializeField] float MaxZ;
        [SerializeField] bool CanSpawn = true;
        float timer;

        void Update()
        {
            timer += Time.deltaTime % 60;
            if (timer < TimeToSpawn || !CanSpawn)
                return;

            var position = new Vector3(Random.Range(MinX,MaxX),8, Random.Range(MinZ,MaxZ));

            
            Instantiate(Enemy1,position, Quaternion.identity);
                
            timer = 0;
        }
    }
}
