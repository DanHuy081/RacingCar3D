using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessLevelHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] sectionsPrefabs;

    private GameObject[] sectionsPool = new GameObject[20];
    private GameObject[] sections = new GameObject[10];
    private Transform playerCarTransform;

    private WaitForSeconds waitFor10ms = new WaitForSeconds(0.1f);
    private const float sectionLength = 26f;

    void Start()
    {
        playerCarTransform = GameObject.FindGameObjectWithTag("Player").transform;

        int prefabIndex = 0;

        // Tạo pool từ các prefab section
        for (int i = 0; i < sectionsPool.Length; i++)
        {
            sectionsPool[i] = Instantiate(sectionsPrefabs[prefabIndex]);
            sectionsPool[i].SetActive(false);

            prefabIndex++;
            if (prefabIndex >= sectionsPrefabs.Length)
                prefabIndex = 0;
        }

        // Kích hoạt các section đầu tiên
        for (int i = 0; i < sections.Length; i++)
        {
            GameObject randomSection = GetRandomSectionFromPool();
            randomSection.transform.position = new Vector3(0, -10, i * sectionLength);
            randomSection.SetActive(true);
            sections[i] = randomSection;
        }

        StartCoroutine(UpdateLessOftenCO());
    }

    IEnumerator UpdateLessOftenCO()
    {
        while (true)
        {
            UpdateSectionPositions();
            yield return waitFor10ms;
        }
    }

    void UpdateSectionPositions()
    {
        for (int i = 0; i < sections.Length; i++)
        {
            if (sections[i].transform.position.z - playerCarTransform.position.z < -sectionLength)
            {
                Vector3 lastPos = sections[i].transform.position;
                sections[i].SetActive(false);

                GameObject newSection = GetRandomSectionFromPool();
                newSection.transform.position = new Vector3(0, -10, lastPos.z + sectionLength * sections.Length);
                newSection.SetActive(true);
                sections[i] = newSection;
            }
        }
    }

    GameObject GetRandomSectionFromPool()
    {
        int randomIndex = UnityEngine.Random.Range(0, sectionsPool.Length);


        while (sectionsPool[randomIndex].activeInHierarchy)
        {
            randomIndex++;
            if (randomIndex >= sectionsPool.Length)
                randomIndex = 0;
        }

        return sectionsPool[randomIndex];
    }
}
