using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private float dropSpeed;
    [SerializeField] private ItemIcon[] icons;

    [NonSerialized] private readonly Item[] inventory = new Item[5];

    public bool Add(Item itemToAdd)
    {
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

    public void Drop()
    {
        int randIndex = Random.Range(0, inventory.Length - 1);
        for (int i = 0; i < 100; i++) //todo better implementation
        {
            if (inventory[randIndex] != null) break;
            randIndex = Random.Range(0, inventory.Length - 1);

            if (i == 99) return;
        }

        Drop(randIndex);
    }

    private void Drop(int index)
    {
        if (inventory[index] == null) return;

        GameObject dropItem = Instantiate(itemPrefab);
        dropItem.transform.position = (Vector2) transform.position + new Vector2(0f, 2f);
        dropItem.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-1f, 1f), 1f).normalized * dropSpeed;
        ItemBehavior itemBehavior = dropItem.GetComponent<ItemBehavior>();
        itemBehavior.item = inventory[index];
        itemBehavior.SetItemValues();
        inventory[index] = null;

        UpdateUI();
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