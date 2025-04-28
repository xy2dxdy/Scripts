using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapChunkGenerator : MonoBehaviour
{
    public GameObject[] chunkPrefabs; // Префабы чанков
    public GameObject[] obstacles; // Префабы препятствий
    public GameObject[] bonuses; // Префабы бонусов
    public GameObject[] coins; // Префабы монет
    public float chunkSize = 50f; // Размер одного чанка
    public int initialChunks = 9; // Количество начальных чанков
    public int numObstacles = 10; // Количество препятствий на чанк
    public int numBonuses = 5; // Количество бонусов на чанк
    public int numCoins = 5; // Количество монет на чанк

    public List<GameObject> activeChunks = new List<GameObject>(); // Список активных чанков
    public Vector3 lastPlayerChunkPosition; // Последняя позиция игрока на чанке
    public Transform playerTransform; // Ссылка на трансформ игрока

    public void Start()
    {
        // Найти объект игрока по тегу
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Player not found! Please ensure the player object has the 'Player' tag.");
        }

        // Спавн начальных чанков
        Vector3 initialPosition = Vector3.zero;
        SpawnInitialChunks(initialPosition);
        lastPlayerChunkPosition = initialPosition;
    }

    public void Update()
    {
        if (playerTransform == null)
        {
            return;
        }

        Vector3 currentChunkPosition = GetChunkPosition(playerTransform.position);

        if (currentChunkPosition != lastPlayerChunkPosition)
        {
            SpawnAdjacentChunks(currentChunkPosition);
            RemoveOldChunks(currentChunkPosition);
            lastPlayerChunkPosition = currentChunkPosition;
        }
    }

    public Vector3 GetChunkPosition(Vector3 position)
    {
        int x = Mathf.FloorToInt(position.x / chunkSize) * (int)chunkSize;
        int z = Mathf.FloorToInt(position.z / chunkSize) * (int)chunkSize;
        return new Vector3(x, 0, z);
    }

    public void SpawnInitialChunks(Vector3 centerChunkPosition)
    {
        List<Vector3> initialPositions = new List<Vector3>
        {
            centerChunkPosition + new Vector3(chunkSize, 0, 0),
            centerChunkPosition + new Vector3(-chunkSize, 0, 0),
            centerChunkPosition + new Vector3(0, 0, chunkSize),
            centerChunkPosition + new Vector3(0, 0, -chunkSize),
            centerChunkPosition + new Vector3(chunkSize, 0, chunkSize),
            centerChunkPosition + new Vector3(chunkSize, 0, -chunkSize),
            centerChunkPosition + new Vector3(-chunkSize, 0, chunkSize),
            centerChunkPosition + new Vector3(-chunkSize, 0, -chunkSize),
            centerChunkPosition // Центр, текущая позиция чанка
        };

        foreach (var position in initialPositions)
        {
            if (!IsChunkAtPosition(position))
            {
                SpawnChunk(position);
            }
        }
    }

    public void SpawnAdjacentChunks(Vector3 centerChunkPosition)
    {
        List<Vector3> potentialPositions = new List<Vector3>
        {
            centerChunkPosition + new Vector3(chunkSize, 0, 0),
            centerChunkPosition + new Vector3(-chunkSize, 0, 0),
            centerChunkPosition + new Vector3(0, 0, chunkSize),
            centerChunkPosition + new Vector3(0, 0, -chunkSize),
            centerChunkPosition + new Vector3(chunkSize, 0, chunkSize),
            centerChunkPosition + new Vector3(chunkSize, 0, -chunkSize),
            centerChunkPosition + new Vector3(-chunkSize, 0, chunkSize),
            centerChunkPosition + new Vector3(-chunkSize, 0, -chunkSize),
            centerChunkPosition // Центр, текущая позиция чанка
        };

        foreach (var position in potentialPositions)
        {
            if (!IsChunkAtPosition(position))
            {
                SpawnChunk(position);
            }
        }
    }

    public void SpawnChunk(Vector3 spawnPosition)
    {
        if (IsChunkAtPosition(spawnPosition))
        {
            return;
        }

        GameObject chunk = Instantiate(chunkPrefabs[Random.Range(0, chunkPrefabs.Length)], spawnPosition, Quaternion.identity);
        activeChunks.Add(chunk);
        GenerateObjectsOnChunk(chunk);
    }

    public void GenerateObjectsOnChunk(GameObject chunk)
    {
        for (int i = 0; i < numObstacles; i++)
        {
            Vector3 randomPosition = chunk.transform.position + new Vector3(Random.Range(-chunkSize / 2, chunkSize / 2), 0, Random.Range(-chunkSize / 2, chunkSize / 2));
            Instantiate(obstacles[Random.Range(0, obstacles.Length)], randomPosition, Quaternion.identity, chunk.transform);
        }

        for (int i = 0; i < numBonuses; i++)
        {
            Vector3 randomPosition = chunk.transform.position + new Vector3(Random.Range(-chunkSize / 2, chunkSize / 2), 0, Random.Range(-chunkSize / 2, chunkSize / 2));
            Instantiate(bonuses[Random.Range(0, bonuses.Length)], randomPosition, Quaternion.identity, chunk.transform);
        }

        for (int i = 0; i < numCoins; i++)
        {
            Vector3 randomPosition = chunk.transform.position + new Vector3(Random.Range(-chunkSize / 2, chunkSize / 2), 0, Random.Range(-chunkSize / 2, chunkSize / 2));
            Instantiate(coins[Random.Range(0, coins.Length)], randomPosition, Quaternion.identity, chunk.transform);
        }
    }


    public bool IsChunkAtPosition(Vector3 position)
    {
        foreach (var chunk in activeChunks)
        {
            if (chunk.transform.position == position)
            {
                return true;
            }
        }
        return false;
    }

    public void RemoveOldChunks(Vector3 centerChunkPosition)
    {
        List<GameObject> chunksToRemove = new List<GameObject>();

        foreach (var chunk in activeChunks)
        {
            if (Vector3.Distance(chunk.transform.position, centerChunkPosition) > chunkSize * 1.5f)
            {
                chunksToRemove.Add(chunk);
            }
        }

        foreach (var chunk in chunksToRemove)
        {
            activeChunks.Remove(chunk);
            Destroy(chunk);
        }
    }

    public int GetActiveChunkCount()
    {
        return activeChunks.Count;
    }
}
