using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab; // Arraste o Prefab do inimigo para cá
    [SerializeField] private float spawnInterval = 30f;
    [SerializeField] private Transform[] spawnPoints; // Pontos onde os inimigos podem aparecer
    
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f; // Reinicia o cronômetro
        }
    }

    void SpawnEnemy()
    {
        if (spawnPoints.Length == 0) return;

        // Escolhe um ponto de spawn aleatório
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(enemyPrefab, spawnPoints[randomIndex].position, Quaternion.identity);
    }
}
