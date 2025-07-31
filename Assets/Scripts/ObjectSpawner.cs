using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ObjectSpawner : MonoBehaviour
{

    [SerializeField] private LayerMask roadLayer;

    [Header("Spawn Settings")]
    public GameObject[] collectiblePrefabs;
    public GameObject[] obstaclePrefabs;
    public float spawnInterval = 0.3f;
    public float distanceAhead = 50f;

    [Header("Lanes")]
    public float[] lanePositionsX = { -2.5f, 0f, 2.5f };

    [Header("Cleanup")]
    public float destroyBehindDistance = 30f;

    private Transform player;
    private List<GameObject> spawnedObjects = new List<GameObject>();

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            SpawnRandomObject();
            CleanupOldObjects();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnRandomObject()
    {
        bool spawnCollectible = Random.Range(0, 2) == 0;

        GameObject[] pool = spawnCollectible ? collectiblePrefabs : obstaclePrefabs;
        if (pool.Length == 0) return;

        GameObject prefab = pool[Random.Range(0, pool.Length)];

        float randomX = lanePositionsX[Random.Range(0, lanePositionsX.Length)];
        float spawnZ = player.position.z + distanceAhead;

        // 👉 Dùng raycast để tìm mặt đất
        Vector3 rayOrigin = new Vector3(randomX, player.position.y + 10f, spawnZ);
        Ray ray = new Ray(rayOrigin, Vector3.down);
        RaycastHit hit;

        Vector3 spawnPos;

        if (Physics.Raycast(ray, out hit, 100f))
        {
            spawnPos = hit.point + Vector3.up * 0.5f; // spawn cao hơn mặt đất 0.5
        }
        else
        {
            Debug.LogWarning("❗ Không tìm thấy mặt đất để spawn vật phẩm.");
            return;
        }

        GameObject obj = Instantiate(prefab, spawnPos, Quaternion.identity);
        spawnedObjects.Add(obj);

        Debug.Log($"✔️ Spawned {prefab.name} at {spawnPos}");
    }


    void CleanupOldObjects()
    {
        for (int i = spawnedObjects.Count - 1; i >= 0; i--)
        {
            if (spawnedObjects[i] == null) continue;

            if (spawnedObjects[i].transform.position.z < player.position.z - destroyBehindDistance)
            {
                Destroy(spawnedObjects[i]);
                spawnedObjects.RemoveAt(i);
            }
        }
    }
}
