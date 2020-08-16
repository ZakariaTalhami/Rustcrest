using UnityEngine;
using System.Collections.Generic;
using System.Linq;

// TODO: Needs lots of refactoring
public class InventoryController : MonoBehaviour {
    
    public static InventoryController instance;

    public int inventorySize = 30;
    public List<Item> inventory;

    private void Start() {
        if(instance != null && instance != this) {
            Destroy(gameObject);
        } else {
            instance = this;
            inventory = new List<Item>();
        }
    }

    public int GetItemCount(string slug)
    {
        return inventory.Count(item => item.slugName == slug);
    }

    public bool GiveItem(Item itemToGive) {
        bool hasRoom = false;
        if(inventory.Count < inventorySize)
        {
            hasRoom = true;
            inventory.Add(itemToGive);
            UIEventHandler.ItemAddedToInventory(itemToGive);
        }

        return hasRoom;
    }

    // Give Item without updating the UI 
    public bool AddItem(Item itemToGive) {
        bool hasRoom = false;
        if(inventory.Count < inventorySize)
        {
            hasRoom = true;
            inventory.Add(itemToGive);
        }

        return hasRoom;
    }

    public bool dropItem(Item item)
    {
        if(inventory.Contains(item) && isItemDropable(item)) 
        {
            inventory.Remove(item);
            GameObject itemGO = Instantiate(item.prefab, this.transform.position + (this.transform.forward * 0.5f), Quaternion.identity);
            Rigidbody itemRB = itemGO.transform.GetComponent<Rigidbody>();
            itemRB.AddForce((this.transform.forward * 0.5f ), ForceMode.Impulse);
            UIEventHandler.ItemDroppedFromInventory(item);
            return true;
        }

        return false;        
    }

    public bool OfferItem(Item item)
    {
        bool removed = RemoveItem(item);
        if(removed)
        {
            UIEventHandler.ItemRemovedFromInventory(item);
        }

        return removed;
    }

    public bool RemoveItem(Item item)
    {
        if(inventory.Contains(item) && isItemDropable(item)) 
        {
            inventory.Remove(item);
            return true;
        }

        return false;        
    }

    public bool isItemDropable(Item item) 
    {
        return true;
    }

}