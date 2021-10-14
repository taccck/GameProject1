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
    [SerializeField] private float blinkduration = 0.5f;
    [SerializeField] private float blinkdelay = 3f;
    [SerializeField] private GameObject arrowprefab;

    [HideInInspector] private bool started = false;

    private IEnumerator Blink(SpriteRenderer sr)
    {
        while (true)
        {
            sr.enabled = !sr.enabled;
            yield return new WaitForSeconds(blinkduration);
        }
    }

    private IEnumerator Warning(float x)
    {
        yield return new WaitForSeconds(blinkdelay);
        GameObject go = Instantiate(arrowprefab, Camera.main.transform);
        go.transform.localPosition += new Vector3(x, 0, -Camera.main.transform.position.z);
        StartCoroutine(Blink(go.GetComponent<SpriteRenderer>()));
        yield return new WaitForSeconds(spawnDelay - blinkdelay);
        Destroy(go);
    }

    private IEnumerator SpawnEnemy()
    {
        for (int i = 0; i < 1000; i++)
        {
            float randomDistance = Random.Range(-spawnDistance, spawnDistance);

            StartCoroutine(Warning(randomDistance));

            yield return new WaitForSeconds(spawnDelay);
            GameObject newEnemy = Instantiate(enemyToSpawn);
            if (player != null)
                newEnemy.GetComponent<Fallingenemy>().player = player;
            else
                newEnemy.GetComponent<Rigidbody2D>().gravityScale = 1f;
            newEnemy.transform.position = new Vector2(transform.position.x + randomDistance, transform.position.y);
        }
    }

    public void Startme()
    {
        if (!started)
        {
            StartCoroutine(SpawnEnemy());
            started = true;
        }
    }

    public void Stopme()
    {
        if (started)
        {
            StopCoroutine(SpawnEnemy());
            started = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + spawnDistance, transform.position.y));
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x - spawnDistance, transform.position.y));
    }
}