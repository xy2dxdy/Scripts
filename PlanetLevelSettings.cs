using UnityEngine;

[CreateAssetMenu(menuName = "Planet/Level Settings", fileName = "NewPlanetLevelSettings")]
public class PlanetLevelSettings : ScriptableObject
{
    public string planetName;

    public Sprite planetSprite;

    public GameObject[] chunkPrefabs;
    public GameObject[] obstacles;
    public GameObject[] bonuses;
    public GameObject[] coins;

    public float chunkSize = 50f;
    public int numObstacles = 10;
    public int numBonuses = 5;
    public int numCoins = 5;

    public int requiredCoins = 100;
    public int plasmaReward = 10;
}
