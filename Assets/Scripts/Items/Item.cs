using System;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
public class Item
{
    public enum ItemTypes
    {
        Consumable, Quest, Trade
    }

    public string slugName;
    public string itemName;
    public string description;
    public ItemTypes ItemType;
    public bool isStackable;
    public int stackSize;
    public Sprite icon;
    public GameObject prefab;

    [JsonConstructor]
    public Item(string slugName, string itemName, string description, ItemTypes itemType, bool isStackable, int stackSize)
    {
        this.slugName = slugName;
        this.itemName = itemName;
        this.description = description;
        this.ItemType = itemType;
        this.stackSize = stackSize;
        this.isStackable = isStackable;
        this.icon = Resources.Load<Sprite>("Sprites/" + slugName);
        this.prefab = Resources.Load<GameObject>("Prefabs/Items/" + slugName);
    }
}