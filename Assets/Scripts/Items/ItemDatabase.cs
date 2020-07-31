using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase instance { get; set; }
    private List<Item> items { get; set; }
    private Dictionary<string, Item> itemsMap;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            BuildDatabase();
        }
    }

    private void BuildDatabase()
    {
        items = JsonConvert.DeserializeObject<List<Item>>(Resources.Load<TextAsset>("JSON/Items").ToString());
        createSlugItemMap();
    }

    private void createSlugItemMap()
    {
        itemsMap = new Dictionary<string, Item>();
        foreach (Item item in items)
        {
            itemsMap.Add(item.slugName, item);
        }
    }

    public Item getItem(string slug)
    {
        Item foundItem;
        itemsMap.TryGetValue(slug, out foundItem);
        return foundItem;
    }

}