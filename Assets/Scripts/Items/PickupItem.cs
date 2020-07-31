using UnityEngine;

public class PickupItem : Interactable
{

    public string itemSlug;
    public Item item;

    private void Start()
    {
        item = ItemDatabase.instance.getItem(itemSlug);
    }

    public override void Interacte()
    {
        if (InventoryController.instance.GiveItem(item))
        {
            Debug.Log("Picking up " + item.itemName.ToString() + "!");
            Destroy(gameObject);
        }
    }


}