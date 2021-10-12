using System.Collections;
using FG;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject enemyToSpawn;
    [SerializeField] private float spawnDelay = 5f;

    private IEnumerator SpawnEnemy()
    {
        for (int i = 0; i < 1000; i++)
        {
            yield return new WaitForSeconds(spawnDelay);
            GameObject newEnemy = Instantiate(enemyToSpawn);
            newEnemy.GetComponent<Fallingenemy>().player = player;
            newEnemy.transform.position = transform.position;
        }
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }
}
