using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private float dropSpeed;

    [NonSerialized] private Item[] inventory = new Item[5];

    public void Add(Item itemToAdd)
    {
        for (int i = 0; i < 5; i++)
        {
            if (inventory[i] != null) continue;

            inventory[i] = itemToAdd;
            break;
        }
    }

    public void Drop()
    {
        int randIndex = Random.Range(0, inventory.Length - 1);
        for (int i = 0; i < 100; i++) //todo better implementation
        {
            if (inventory[randIndex] != null) break;
            randIndex = Random.Range(0, inventory.Length - 1);
            
            if (i == 99) return;
        }

        GameObject dropItem = Instantiate(itemPrefab);
        dropItem.transform.position = (Vector2) transform.position + new Vector2(0f, 2f);
        dropItem.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-1f, 1f), 1f).normalized * dropSpeed;
        ItemBehavior itemBehavior = dropItem.GetComponent<ItemBehavior>();
        itemBehavior.item = inventory[randIndex];
        itemBehavior.SetItemValues();
        inventory[randIndex] = null;
    }
}