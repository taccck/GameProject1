using System;
using System.Linq;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Curr { get; private set; }

    public SaveFile saveFile;
    [SerializeField] private Transform hotSauce;

    private PlayerInventory inventory;

    public void Clear()
    {
        saveFile.first = true;
        saveFile.items = null;
        saveFile.playerPos = Vector2.zero;
        saveFile.lavaHeight = 0;
    }

    public void Save(Vector2 position)
    {
        if (hotSauce == null)
        {
            Debug.LogError("hot sauce reference is not set in save manager on the player");
            return;
        }

        saveFile.first = false;
        saveFile.playerPos = position;
        saveFile.items = inventory.inventory.ToArray();
        saveFile.lavaHeight = hotSauce.position.y;
    }

    public void Load()
    {
        if (hotSauce == null)
        {
            Debug.LogError("hot sauce reference is not set in save manager on the player");
            return;
        }

        transform.position = saveFile.playerPos;
        inventory.inventory = saveFile.items.ToArray();
        hotSauce.position = new Vector2(0, saveFile.lavaHeight);
    }

    private void Start()
    {
        if (!saveFile.first)
            Load();
    }

    private void Awake()
    {
        inventory = GetComponent<PlayerInventory>();
        Curr = this;
    }

    private void OnApplicationQuit()
    {
        saveFile.first = true;
    }
}