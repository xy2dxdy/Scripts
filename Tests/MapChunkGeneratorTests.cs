using NUnit.Framework;
using UnityEngine;
using System.Collections;
using UnityEngine.TestTools;

public class MapChunkGeneratorTests
{
    private GameObject generatorObject;
    private MapChunkGenerator mapChunkGenerator;
    private GameObject chunkPrefab;
    private GameObject obstaclePrefab;
    private GameObject bonusPrefab;
    private GameObject coinPrefab;
    private GameObject playerObject;
    private GameObject mainCameraObject;

    [SetUp]
    public void Setup()
    {
        generatorObject = new GameObject();
        mapChunkGenerator = generatorObject.AddComponent<MapChunkGenerator>();

        chunkPrefab = new GameObject("Chunk");
        obstaclePrefab = new GameObject("Obstacle");
        bonusPrefab = new GameObject("Bonus");
        coinPrefab = new GameObject("Coin");
        mapChunkGenerator.chunkPrefabs = new GameObject[] { chunkPrefab };
        mapChunkGenerator.obstacles = new GameObject[] { obstaclePrefab };
        mapChunkGenerator.bonuses = new GameObject[] { bonusPrefab };
        mapChunkGenerator.coins = new GameObject[] { coinPrefab };

        playerObject = new GameObject("Player");
        playerObject.tag = "Player";
        playerObject.transform.position = Vector3.zero;

        mainCameraObject = new GameObject("MainCamera");
        var camera = mainCameraObject.AddComponent<Camera>();
        mainCameraObject.tag = "MainCamera";

        camera.transform.position = new Vector3(0, 10, -10);
        camera.transform.LookAt(Vector3.zero);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(generatorObject);
        Object.DestroyImmediate(chunkPrefab);
        Object.DestroyImmediate(obstaclePrefab);
        Object.DestroyImmediate(bonusPrefab);
        Object.DestroyImmediate(coinPrefab);
        Object.DestroyImmediate(playerObject);
        Object.DestroyImmediate(mainCameraObject);
    }

    [UnityTest]
    public IEnumerator MapChunkGenerator_SpawnsInitialChunks()
    {
        mapChunkGenerator.Start();

        yield return null;

        Assert.AreEqual(mapChunkGenerator.initialChunks, mapChunkGenerator.GetActiveChunkCount());
    }
}
