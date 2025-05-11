using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform player;
    public float spawnInterval = 3f;
    public float enemyLifetime = 10f;
    public int poolSize = 10;

    public float spawnDistance = 50f;
    public float spawnOffsetRange = 5f;

    private List<GameObject> enemyPool = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, Vector3.zero, Quaternion.identity);
            enemy.SetActive(false);
            enemyPool.Add(enemy);
        }
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnEnemy()
    {
        // �������� ��������� �������: 0 � ����, 1 � ���, 2 � ����, 3 � �����.
        int randomSide = Random.Range(0, 4);
        Vector3 spawnPosition = Vector3.zero;
        switch (randomSide)
        {
            case 0: // ����: ����� �� ��� Z ���� ������ 
                spawnPosition = new Vector3(
                    player.position.x + Random.Range(-spawnOffsetRange, spawnOffsetRange),
                    player.position.y,
                    player.position.z + spawnDistance);
                break;
            case 1: // ���: ����� ���� ������ 
                spawnPosition = new Vector3(
                    player.position.x + Random.Range(-spawnOffsetRange, spawnOffsetRange),
                    player.position.y,
                    player.position.z - spawnDistance);
                break;
            case 2: // ����: ����� ����� �� ������ 
                spawnPosition = new Vector3(
                    player.position.x - spawnDistance,
                    player.position.y,
                    player.position.z + Random.Range(-spawnOffsetRange, spawnOffsetRange));
                break;
            case 3: // �����: ����� ������ �� ������ 
                spawnPosition = new Vector3(
                    player.position.x + spawnDistance,
                    player.position.y,
                    player.position.z + Random.Range(-spawnOffsetRange, spawnOffsetRange));
                break;
        }

        GameObject enemy = GetPooledEnemy();
        if (enemy != null)
        {
            enemy.transform.position = spawnPosition;
            enemy.transform.rotation = Quaternion.identity;
            enemy.SetActive(true);

            StartCoroutine(DisableAfterTime(enemy, enemyLifetime));
        }
    }

    GameObject GetPooledEnemy()
    {
        foreach (var enemy in enemyPool)
        {
            if (!enemy.activeInHierarchy)
            {
                return enemy;
            }
        }

        GameObject newEnemy = Instantiate(enemyPrefab, Vector3.zero, Quaternion.identity);
        newEnemy.SetActive(false);
        enemyPool.Add(newEnemy);
        return newEnemy;
    }

    IEnumerator DisableAfterTime(GameObject enemy, float delay)
    {
        yield return new WaitForSeconds(delay);
        enemy.SetActive(false);
    }

    public void SetPlayerTarget(Transform newPlayer)
    {
        player = newPlayer;
    }
}
