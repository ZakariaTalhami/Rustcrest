using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;


[Serializable]
public class Slot : MonoBehaviour, IDropHandler
{
    private enum DropType
    {
        Deposit, Loot, DepositAndLoot, Swap, Move
    };

    public bool isInventorySlot = true;
    public SlotItemData item { get; set; }
    public int index;

    private SlotItemData sltoItemPrefab;

    private void Awake()
    {
        sltoItemPrefab = Resources.Load<SlotItemData>("UI/Item");
    }

    public void SetContent(Item item)
    {
        this.item = Instantiate(sltoItemPrefab, gameObject.transform);
        this.item.isInventoryItem = isInventorySlot;
        this.item.SetItem(item);
    }

    public void SetContent(Item item, int amount)
    {
        this.item = Instantiate(sltoItemPrefab, gameObject.transform);
        this.item.isInventoryItem = isInventorySlot;
        this.item.SetItem(item, amount);
    }

    public void ClearContent()
    {
        this.item.remove();
        this.item = null;
    }

    public bool isEmpty()
    {
        return item == null;
    }


    public void OnDrop(PointerEventData eventData)
    {
        SlotItemData droppedItemData = eventData.pointerDrag.GetComponent<SlotItemData>();

        DropType dropType;
        if (droppedItemData.isInventoryItem)
        {
            if (this.isInventorySlot)
            {
                if (this.isEmpty())
                {
                    dropType = DropType.Move;
                }
                else
                {
                    dropType = DropType.Swap;
                }
            }
            else
            {
                if (this.isEmpty())
                {
                    dropType = DropType.Deposit;
                }
                else
                {
                    dropType = DropType.DepositAndLoot;
                }
            }
        }
        else
        {
            if (this.isInventorySlot)
            {
                if (this.isEmpty())
                {
                    dropType = DropType.Loot;
                }
                else
                {
                    dropType = DropType.DepositAndLoot;
                }
            }
            else
            {
                if (this.isEmpty())
                {
                    dropType = DropType.Move;
                }
                else
                {
                    dropType = DropType.Swap;
                }
            }
        }

        PerformDropOperation(this.item, droppedItemData, dropType);
    }

    private delegate bool ItemOperation(Item item);
    private void PerformDropOperation(SlotItemData slotItem, SlotItemData droppedItem, DropType dropType)
    {
        switch (dropType)
        {
            case DropType.Move:
                MoveItem(droppedItem);
                break;
            case DropType.Swap:
                SwapItems(slotItem, droppedItem);
                break;
            case DropType.Deposit:
                RepeatItemOperation(InventoryController.instance.RemoveItem, droppedItem.item, droppedItem.amount);
                RepeatItemOperation(ContainerController.Instance.DepositWithoutUI, droppedItem.item, droppedItem.amount);
                MoveItem(droppedItem);
                break;
            case DropType.Loot:
                RepeatItemOperation(ContainerController.Instance.Withdraw, droppedItem.item, droppedItem.amount);
                RepeatItemOperation(InventoryController.instance.AddItem, droppedItem.item, droppedItem.amount);
                MoveItem(droppedItem);
                break;  
            case DropType.DepositAndLoot:
                if (this.isInventorySlot)
                {
                    RepeatItemOperation(ContainerController.Instance.Withdraw, droppedItem.item, droppedItem.amount);
                    RepeatItemOperation(InventoryController.instance.AddItem, droppedItem.item, droppedItem.amount);

                    RepeatItemOperation(InventoryController.instance.RemoveItem, slotItem.item, slotItem.amount);
                    RepeatItemOperation(ContainerController.Instance.DepositWithoutUI, slotItem.item, slotItem.amount);
                }
                else
                {
                    RepeatItemOperation(ContainerController.Instance.Withdraw, slotItem.item, slotItem.amount);
                    RepeatItemOperation(InventoryController.instance.AddItem, slotItem.item, slotItem.amount);

                    RepeatItemOperation(InventoryController.instance.RemoveItem, droppedItem.item, droppedItem.amount);
                    RepeatItemOperation(ContainerController.Instance.DepositWithoutUI, droppedItem.item, droppedItem.amount);
                }
                SwapItems(slotItem, droppedItem);
                break;
        }
    }

    private void RepeatItemOperation(ItemOperation func, Item item, int repeatCount)
    {
        for (int i = 0; i < repeatCount; i++)
        {
            func(item);
        }
    }

    private void MoveItem(SlotItemData droppedItem)
    {
        SetContent(droppedItem.item, droppedItem.amount);
        droppedItem.remove();
    }

    private void SwapItems(SlotItemData slotItem, SlotItemData droppedItem)
    {
        Item droppedItemtemp = droppedItem.item;
        int droppedAmounttemp = droppedItem.amount;
        droppedItem.SetItem(slotItem.item, slotItem.amount);
        slotItem.SetItem(droppedItemtemp, droppedAmounttemp);
    }
}