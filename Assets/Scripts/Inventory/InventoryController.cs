using UnityEngine;
using System.Collections.Generic;

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
            return true;
        }

        return false;        
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