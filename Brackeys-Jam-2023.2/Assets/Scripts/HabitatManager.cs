/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabitatManager : MonoBehaviour
{
    public GameObject[] fishPrefabs; // Array de prefabs de peixes
    public Vector2 spawnAreaSize = new Vector2(10f, 10f); // Tamanho da área de spawn
    public int minFishCount = 5; // Contagem mínima de peixes por tipo
    public int maxFishCount = 10; // Contagem máxima de peixes por tipo

    private List<GameObject> spawnedFish = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(ManageFish());
    }




    private IEnumerator ManageFish()
    {
        while (true)
        {
            foreach (GameObject fishPrefab in fishPrefabs)
            {
                int currentFishCount = CountFishOfType(fishPrefab);

                if (currentFishCount < minFishCount)
                {
                    SpawnFish(fishPrefab);
                }
                else if (currentFishCount > maxFishCount)
                {
                    DespawnFish(fishPrefab);
                }
            }

            yield return new WaitForSeconds(1f);
        }
    }

    private int CountFishOfType(GameObject fishPrefab)
    {
        int count = 0;
        foreach (GameObject fish in spawnedFish)
        {
            if (fish.GetComponent<FishController>().fishPrefab == fishPrefab)
            {
                count++;
            }
        }
        return count;
    }

    private void SpawnFish(GameObject fishPrefab)
    {
        Vector2 spawnPosition = GetRandomSpawnPosition();
        GameObject newFish = Instantiate(fishPrefab, spawnPosition, Quaternion.identity);
        newFish.transform.SetParent(transform);
        spawnedFish.Add(newFish);
    }

    private void DespawnFish(GameObject fishPrefab)
    {
        for (int i = spawnedFish.Count - 1; i >= 0; i--)
        {
            GameObject fish = spawnedFish[i];
            if (fish.GetComponent<FishController>().fishPrefab == fishPrefab)
            {
                spawnedFish.RemoveAt(i);
                Destroy(fish);
                return;
            }
        }
    }

    private Vector2 GetRandomSpawnPosition()
    {
        float x = Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2);
        float y = Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2);
        return new Vector2(x, y);
    }
}
*/