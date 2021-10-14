using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerInventory : MonoBehaviour
{
    public Item[] inventory = new Item[5];

    [NonSerialized] public bool CanPickup = true;

    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private float dropSpeed;
    [SerializeField] private ItemIcon[] icons;
    [SerializeField] private Image outcome;
    [SerializeField] private Text nothingText;
    [SerializeField] private RordonSpeech rordon;

    private LayerMask floorMask;

    private const float DROP_OFFSET = 1.5f;

    public bool Add(Item itemToAdd)
    {
        if (!CanPickup) return false;

        for (int i = 0; i < 5; i++)
        {
            if (inventory[i] != null) continue;

            inventory[i] = itemToAdd;
            UpdateUI();
            rordon.StartRordonLine();
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

    public void UpdateUI()
    {
        for (int i = 0; i < 5; i++)
        {
            icons[i].SetUI(inventory[i]);
        }

        Recipes.Recipe r = Recipes.current.GetRecipe(inventory);
        if (r.name == "WTF have you done!")
        {
            outcome.color = new Color(0, 0, 0, 0);
            nothingText.enabled = true;
            return;
        }

        nothingText.enabled = false;
        outcome.color = new Color(1, 1, 1, 1);
        outcome.sprite = Recipes.current.GetRecipe(inventory).sprite;
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