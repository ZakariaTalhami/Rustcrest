using System.Collections.Generic;
using UnityEngine;

public class ContainerItem : Interactable {

    public new string name;
    public int size;

    [SerializeField]
    private List<string> initialItemsSlug;

    public List<Item> items;


    private void Start() {
        foreach (string slug in initialItemsSlug)
        {
            items.Add(ItemDatabase.instance.getItem(slug));
        }
    }

    public override void Interacte()
    {
        ContainerController.Instance.OpenContainer(this);
    }

    public bool removeItem(Item item) {
        return items.Remove(item);
    }

    public bool additem(Item item)
    {
        bool wasAdded = false;
        if(items.Count < size)
        {
            items.Add(item);
            wasAdded = true;
        }
        return wasAdded;
    }
}