using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnCollectables : MonoBehaviour
{
    [System.Serializable]
    private class SpawnItem
    {
        public string name;
        public GameObject prefab;

        [Range(1, 100)]
        public int rarityWeight = 10;

        public int maxCount = 5;
        public int currentCount = 0;
    }

    [SerializeField] private List<SpawnItem> spawnItems;

    [SerializeField] private Vector3 areaSize = new (3f, 3f, 0f);

    [SerializeField] private float minSpawnTime = 10f;
    [SerializeField] private float maxSpawnTime = 30f;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));

            SpawnItem selectedItem = GetRandomItem();

            if (selectedItem != null)
            {
                Spawn(selectedItem);
            }
        }
    }

    private SpawnItem GetRandomItem()
    {
        List<SpawnItem> availableItems = new List<SpawnItem>();

        int totalWeight = 0;

        foreach (SpawnItem item in spawnItems)
        {
            if (item.currentCount < item.maxCount)
            {
                availableItems.Add(item);
                totalWeight += item.rarityWeight;
            }
        }

        if (availableItems.Count == 0)
            return null;

        int randomValue = Random.Range(0, totalWeight);

        int currentWeight = 0;

        foreach (SpawnItem item in availableItems)
        {
            currentWeight += item.rarityWeight;

            if (randomValue < currentWeight)
            {
                return item;
            }
        }

        return null;
    }

    private void Spawn(SpawnItem item)
    {
        Vector3 randomPosition = transform.position + new Vector3(
            Random.Range(-areaSize.x / 2, areaSize.x / 2), 0, Random.Range(-areaSize.z / 2, areaSize.z / 2)
        );

        GameObject obj = Instantiate(item.prefab, randomPosition, Quaternion.identity);

        item.currentCount++;

        StartCoroutine(WaitForDestroy(obj, item));
    }

    private IEnumerator WaitForDestroy(GameObject obj, SpawnItem item)
    {
        yield return new WaitUntil(() => obj == null);

        item.currentCount--;
    }
}