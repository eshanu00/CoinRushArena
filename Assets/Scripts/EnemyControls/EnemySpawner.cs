using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRate = 8.0f; 
    public int maxEnemies = 3; 
    private float nextSpawn = 0.0f;

    void Update()
    {
        
        int currentEnemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        
        if (currentEnemyCount < maxEnemies && Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;

            float randomX = Random.Range(-6f, 6f);
            float randomY = Random.Range(-3f, 3f);

            Vector2 spawnPosition = new Vector2(randomX, randomY);
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }
}