using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabitatManager : MonoBehaviour
{
    public GameObject[] fishPrefabs; // Array de prefabs de peixes
    public Vector2 spawnAreaSize = new Vector2(10f, 10f); // Tamanho da área de spawn
    public int minFishCount = 2; // Contagem mínima de peixes por tipo
    public int maxFishCount = 5; // Contagem máxima de peixes por tipo



    private List<GameObject> spawnedFish = new List<GameObject>();

    private void Awake() {
        AudioManager.instance.StopSound("menu");
        AudioManager.instance.PlaySound("ambiente");
    }

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
                int currentFishCount = CountFishOfType(fishPrefab.tag);

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

    private int CountFishOfType(string fishTag)
{
    int count = 0;
    foreach (GameObject spawnedFish in spawnedFish)
    {
        if (spawnedFish != null && spawnedFish.CompareTag(fishTag))
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
            if (fish.gameObject == fishPrefab)      
            {
                spawnedFish.RemoveAt(i);
                Destroy(fish);
                return;
            }
        }
    }

    private Vector2 GetRandomSpawnPosition()
    {
        Vector2 randomPosition = Vector2.zero;
        int maxAttempts = 10; // To avoid infinite loop

        for (int i = 0; i < maxAttempts; i++)
        {
            float x = Random.Range(transform.position.x - spawnAreaSize.x / 2, transform.position.x + spawnAreaSize.x / 2);
            float y = Random.Range(transform.position.y - spawnAreaSize.y / 2, transform.position.y + spawnAreaSize.y / 2);
            randomPosition = new Vector2(x, y);

            Collider2D collider = Physics2D.OverlapCircle(randomPosition, 0.1f); // Check for colliders at the potential spawn point

            if (collider != null && collider.gameObject.layer == LayerMask.NameToLayer("Water"))
            {
                return randomPosition;
            }
        }

        return randomPosition;
    }
}
