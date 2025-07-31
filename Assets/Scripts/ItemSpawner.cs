using UnityEngine;
using System.Collections.Generic;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] itemPrefabs;
    public Transform[] spawnPoints;
    public float spawnInterval = 4f;
    public float chancePerSpawn = 0.5f; // tỉ lệ có thể không spawn ở mỗi điểm

    void Start()
    {
        InvokeRepeating(nameof(SpawnItems), 1f, spawnInterval);
    }

    void SpawnItems()
    {
        foreach (var point in spawnPoints)
        {
            if (Random.value > chancePerSpawn) continue;

            int prefabIndex = Random.Range(0, itemPrefabs.Length);
            Instantiate(itemPrefabs[prefabIndex], point.position, Quaternion.identity);
        }
    }
}
