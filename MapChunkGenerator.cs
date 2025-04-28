using System.Collections.Generic;
using UnityEngine;

public class MapChunkGenerator : MonoBehaviour
{
    public GameObject[] chunkPrefabs;
    public GameObject[] obstacles;
    public GameObject[] bonuses;
    public GameObject[] coins;

    public float chunkSize = 50f;
    public int initialChunks = 9;
    public int numObstacles = 10;
    public int numBonuses = 5;
    public int numCoins = 5;

    private List<GameObject> activeChunks = new List<GameObject>();
    private Vector3 lastPlayerChunkPosition;
    private Transform playerTransform;

    public void SetupLevelFromPlanet(PlanetLevelSettings settings)
    {
        chunkPrefabs = settings.chunkPrefabs;
        obstacles = settings.obstacles;
        bonuses = settings.bonuses;
        coins = settings.coins;
        chunkSize = settings.chunkSize;
        numObstacles = settings.numObstacles;
        numBonuses = settings.numBonuses;
        numCoins = settings.numCoins;
    }

    void Start()
    {
        if (SceneDataManager.Instance.selectedPlanetSettings != null)
        {
            
            SetupLevelFromPlanet(SceneDataManager.Instance.selectedPlanetSettings);
        }
        else
        {
            Debug.LogError("Не найдены настройки планеты! Используются параметры по умолчанию.");
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            playerTransform = player.transform;
        else
            Debug.LogError("Игрок не найден! Убедитесь, что у объекта игрока установлен тег 'Player'.");

        Vector3 initialPosition = Vector3.zero;
        SpawnInitialChunks(initialPosition);
        lastPlayerChunkPosition = initialPosition;
    }

    void Update()
    {
        if (playerTransform == null)
            return;

        Vector3 currentChunkPosition = GetChunkPosition(playerTransform.position);
        if (currentChunkPosition != lastPlayerChunkPosition)
        {
            SpawnAdjacentChunks(currentChunkPosition);
            RemoveOldChunks(currentChunkPosition);
            lastPlayerChunkPosition = currentChunkPosition;
        }
    }

    Vector3 GetChunkPosition(Vector3 position)
    {
        int x = Mathf.FloorToInt(position.x / chunkSize) * (int)chunkSize;
        int z = Mathf.FloorToInt(position.z / chunkSize) * (int)chunkSize;
        return new Vector3(x, 0, z + chunkSize);
    }

    void SpawnInitialChunks(Vector3 centerChunkPosition)
    {
        List<Vector3> positions = new List<Vector3>
        {
            centerChunkPosition + new Vector3(chunkSize, 0, 0),
            centerChunkPosition + new Vector3(-chunkSize, 0, 0),
            centerChunkPosition + new Vector3(0, 0, chunkSize),
            centerChunkPosition + new Vector3(0, 0, -chunkSize),
            centerChunkPosition + new Vector3(chunkSize, 0, chunkSize),
            centerChunkPosition + new Vector3(chunkSize, 0, -chunkSize),
            centerChunkPosition + new Vector3(-chunkSize, 0, chunkSize),
            centerChunkPosition + new Vector3(-chunkSize, 0, -chunkSize),
            centerChunkPosition
        };

        foreach (var pos in positions)
        {
            if (!IsChunkAtPosition(pos))
                SpawnChunk(pos);
        }
    }

    void SpawnAdjacentChunks(Vector3 centerChunkPosition)
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
            centerChunkPosition
        };

        foreach (var pos in potentialPositions)
        {
            if (!IsChunkAtPosition(pos))
                SpawnChunk(pos);
        }
    }

    void SpawnChunk(Vector3 spawnPosition)
    {
        if (IsChunkAtPosition(spawnPosition))
            return;

        GameObject chunk = Instantiate(chunkPrefabs[Random.Range(0, chunkPrefabs.Length)], spawnPosition, Quaternion.identity);
        activeChunks.Add(chunk);

        if (spawnPosition != Vector3.zero)
            GenerateObjectsOnChunk(chunk);
    }

    void GenerateObjectsOnChunk(GameObject chunk)
    {
        for (int i = 0; i < numObstacles; i++)
        {
            Vector3 pos = chunk.transform.position +
                new Vector3(Random.Range(-chunkSize / 2, chunkSize / 2), 0.75f, Random.Range(-chunkSize / 2, chunkSize / 2));
            Instantiate(obstacles[Random.Range(0, obstacles.Length)], pos, Quaternion.Euler(-90, 0, 0), chunk.transform);
        }
        for (int i = 0; i < numBonuses; i++)
        {
            Vector3 pos = chunk.transform.position +
                new Vector3(Random.Range(-chunkSize / 2, chunkSize / 2), 0.75f, Random.Range(-chunkSize / 2, chunkSize / 2));
            Instantiate(bonuses[Random.Range(0, bonuses.Length)], pos, Quaternion.Euler(-90, 0, 0), chunk.transform);
        }
        for (int i = 0; i < numCoins; i++)
        {
            Vector3 pos = chunk.transform.position +
                new Vector3(Random.Range(-chunkSize / 2, chunkSize / 2), 0.75f, Random.Range(-chunkSize / 2, chunkSize / 2));
            Instantiate(coins[Random.Range(0, coins.Length)], pos, Quaternion.Euler(-90, 0, 0), chunk.transform);
        }
    }

    bool IsChunkAtPosition(Vector3 position)
    {
        foreach (var chunk in activeChunks)
        {
            if (chunk.transform.position == position)
                return true;
        }
        return false;
    }

    void RemoveOldChunks(Vector3 centerChunkPosition)
    {
        List<GameObject> toRemove = new List<GameObject>();
        foreach (var chunk in activeChunks)
        {
            if (Vector3.Distance(chunk.transform.position, centerChunkPosition) > chunkSize * 1.5f)
                toRemove.Add(chunk);
        }
        foreach (var chunk in toRemove)
        {
            activeChunks.Remove(chunk);
            Destroy(chunk);
        }
    }
}
