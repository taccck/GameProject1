using UnityEngine;

[CreateAssetMenu(fileName = "New Save", menuName = "ScriptableObjects/Save", order = 1)]
public class SaveFile : ScriptableObject
{
    public bool first = true;
    public Item[] items;
    public Vector2 playerPos;
    public float lavaHeight;
}
