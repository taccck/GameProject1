using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private List<Item> inventory = new List<Item>();

    public void Add(Item i)
    {
        inventory.Add(i);
    }
}