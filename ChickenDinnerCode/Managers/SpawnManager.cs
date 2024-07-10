using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MobSpawnChance
{
    public string levelName; 
    public List<MobSpawnData> mobsSpawnData = new List<MobSpawnData>(); 
}

[System.Serializable]
public class MobSpawnData
{
    public SpawnManager.Mobs mobType; 
    public float spawnChance; 
}

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<MobSpawnChance> levelMobSpawnChances = new List<MobSpawnChance>();
    [SerializeField] private float spawnTimer;
    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();
    private int currentPoint = 0;
    private GameManager gameManager;

    void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        float time = 0;
        while (time < spawnTimer / LevelManager.Instance.difficultyIndex)
        {
            time += Time.deltaTime;
            yield return null;
        }
        
        Spawn();
        StartCoroutine(SpawnObjects());
    }

    void Spawn()
    {
        
        List<MobSpawnData> levelMobsData = DetermineLevelMobsData();  
        MobSpawnData selectedMobData = GetRandomMobData(levelMobsData);
        GameObject spawnedObject = PoolManager.Instance.SpawnFromPool(selectedMobData.mobType.ToString(), spawnPoints[currentPoint].position, Quaternion.identity);
        
        spawnedObject.GetComponent<MobMovementBase>().SetDirection(1 - 2 * currentPoint);

        currentPoint++;
        if (currentPoint == spawnPoints.Count)
        {
            currentPoint = 0;
        }
    }

    List<MobSpawnData> DetermineLevelMobsData()
    {
        
        string currentLevelName = "Level"+LevelManager.Instance.difficultyIndex;

        
        foreach (var levelChance in levelMobSpawnChances)
        {
            if (levelChance.levelName == currentLevelName)
            {
                return levelChance.mobsSpawnData;
            }
        }

        return new List<MobSpawnData>(); 
    }

    MobSpawnData GetRandomMobData(List<MobSpawnData> mobsData)
    {
        float totalChance = 0f;
        foreach (var mobData in mobsData)
        {
            totalChance += mobData.spawnChance;
        }

        float randomValue = Random.Range(0f, totalChance);
        float cumulativeChance = 0f;

        foreach (var mobData in mobsData)
        {
            cumulativeChance += mobData.spawnChance;
            if (randomValue <= cumulativeChance)
            {
                return mobData;
            }
        }

        
        return mobsData[0];
    }

    [System.Serializable]
    public  enum Mobs
    {
        Chicken,
        Sheep,
        Pig,
        Cow
    }
}
