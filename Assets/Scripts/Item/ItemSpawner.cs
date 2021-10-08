using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private SpawnItem[] itemsToSpawn;
    [SerializeField] private GameObject itemPrefab;

    [Serializable]
    private struct SpawnItem
    {
        public Item item;
        [Range(0.0001f, 1f)] public float chance;
    }

    private void Start()
    {
        SpawnRandom();
    }

    private void SpawnRandom()
    {
        float random = Random.Range(0f, 1f);
        float sumOfChances = 0f;
        int randomIndex = 0;
        for (int i = 0; i < itemsToSpawn.Length; i++)
        {
            sumOfChances += itemsToSpawn[i].chance;
            if (random < sumOfChances)
            {
                randomIndex = i;
                break;
            }
        }

        GameObject dropItem = Instantiate(itemPrefab);
        dropItem.transform.position = (Vector2) transform.position;
        ItemBehavior itemBehavior = dropItem.GetComponent<ItemBehavior>();
        itemBehavior.item = itemsToSpawn[randomIndex].item;
        itemBehavior.SetItemValues();
    }

    private void Awake()
    {
        CorrectOdds();
    }

    private void CorrectOdds()
    {
        float sumOfChances = 0f;
        foreach (SpawnItem si in itemsToSpawn)
        {
            sumOfChances += si.chance;
        }

        for (int i = 0; i < itemsToSpawn.Length; i++)
        {
            itemsToSpawn[i].chance /= sumOfChances;
        }
    }
}