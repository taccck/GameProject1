using System.Collections;
using FG;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject enemyToSpawn;
    [SerializeField] private float spawnDelay = 5f;
    [SerializeField] private float spawnDistance = 1f;

    private IEnumerator SpawnEnemy()
    {
        for (int i = 0; i < 1000; i++)
        {
            float randomDistance = Random.Range(-spawnDistance, spawnDistance);
            yield return new WaitForSeconds(spawnDelay);
            GameObject newEnemy = Instantiate(enemyToSpawn);
            newEnemy.GetComponent<Fallingenemy>().player = player;
            newEnemy.transform.position = new Vector2(transform.position.x + randomDistance, transform.position.y);
        }
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + spawnDistance, transform.position.y));
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x - spawnDistance, transform.position.y));
    }
}