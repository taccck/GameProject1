using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerInventory : MonoBehaviour
{
    [NonSerialized] public bool CanPickup = true;
    
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private float dropSpeed;
    [SerializeField] private ItemIcon[] icons;

    private LayerMask floorMask;

    private readonly Item[] inventory = new Item[5];

    private const float DROP_OFFSET = 1.5f;

    public bool Add(Item itemToAdd)
    {
        if (!CanPickup) return false;
        
        for (int i = 0; i < 5; i++)
        {
            if (inventory[i] != null) continue;

            inventory[i] = itemToAdd;
            UpdateUI();
            print(Recipes.current.GetRecipe(inventory).name);
            return true;
        }

        return false;
    }

    public bool Drop()
    {
        List<int> populatedSlots = new List<int>();
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] != null)
                populatedSlots.Add(i);
        }
        if (populatedSlots.Count == 0) return false;
        
        int randIndex = Random.Range(0, populatedSlots.Count - 1);
        randIndex = populatedSlots[randIndex];

        return Drop(randIndex);
    }

    private bool Drop(int index)
    {
        if (inventory[index] == null) return false;
        
        GameObject dropItem = Instantiate(itemPrefab);
        RaycastHit2D hit = Physics2D.Raycast((Vector2) transform.position, Vector2.up, DROP_OFFSET, floorMask);
        float spawnDist = hit ? hit.distance : DROP_OFFSET;

        dropItem.transform.position = (Vector2) transform.position + new Vector2(0f, spawnDist);
        dropItem.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-1f, 1f), 1f).normalized * dropSpeed;
        ItemBehavior itemBehavior = dropItem.GetComponent<ItemBehavior>();
        itemBehavior.waitOnstart = false;
        itemBehavior.item = inventory[index];
        itemBehavior.SetItemValues();
        inventory[index] = null;

        UpdateUI();
        return true;
    }

    private void UpdateUI()
    {
        for (int i = 0; i < 5; i++)
        {
            icons[i].SetUI(inventory[i]);
        }
    }

    private void Start()
    {
        floorMask = LayerMask.GetMask("Floor");
        UpdateUI();
    }

    private void OnDrop1()
    {
        Drop(0);
    }

    private void OnDrop2()
    {
        Drop(1);
    }

    private void OnDrop3()
    {
        Drop(2);
    }

    private void OnDrop4()
    {
        Drop(3);
    }

    private void OnDrop5()
    {
        Drop(4);
    }
}