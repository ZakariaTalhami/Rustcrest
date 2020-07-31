using UnityEngine;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    public RectTransform inventoryPanel;
    public RectTransform slotPanel;

    public RectTransform containerPanel;
    public RectTransform containerSlotPanel;

    public Slot slotPrefab;

    public List<Slot> slots;

    private bool isInventoyOpen = false;
    private bool isContainerOpen = false;
    private List<Slot> containerSlots;

    private void Start()
    {
        inventoryPanel.gameObject.SetActive(isInventoyOpen);
        containerPanel.gameObject.SetActive(isContainerOpen);
        slotPrefab = Resources.Load<Slot>("UI/Slot");
        SetUpEventListeners();
        InstantiateSlots();
    }

    private void SetUpEventListeners()
    {
        UIEventHandler.onItemAddedToInventory += ItemAdded;
        UIEventHandler.onItemAddedToContainer += ItemAddedToContainer;
        UIEventHandler.onContainerOpened += OpenContainer;
        UIEventHandler.onContainerClosed += CloseContainerPanel;
    }

    private void InstantiateSlots()
    {
        slots = new List<Slot>();
        for (int i = 0; i < InventoryController.instance.inventorySize; i++)
        {
            Slot uiSlot = Instantiate(slotPrefab);
            uiSlot.index = i;
            uiSlot.transform.SetParent(slotPanel);
            slots.Add(uiSlot);
        }
    }

    private void InstantiateContainerSlots(ContainerItem container)
    {
        containerSlots = new List<Slot>();
        for (int i = 0; i < container.size; i++)
        {
            Slot uiSlot = Instantiate(slotPrefab);
            uiSlot.isInventorySlot = false;
            uiSlot.index = i;
            uiSlot.transform.SetParent(containerSlotPanel);
            containerSlots.Add(uiSlot);
        }

        foreach (Item item in container.items)
        {
            ItemAddedToContainer(item);
        }


    }

    private void ItemAddedToContainer(Item item)
    {
        if (item.isStackable)
        {
            ItemAddedToExistingSlot(containerSlots, item);
        } 
        else
        {
            ItemAddedToEmptySlot(containerSlots, item);
        }
    }


    private void ItemAdded(Item item)
    {
        if (item.isStackable)
        {
            ItemAddedToExistingSlot(slots, item);
        } 
        else
        {
            ItemAddedToEmptySlot(slots, item);
        }
    }

    private void ItemAddedToExistingSlot(List<Slot> slots, Item item) 
    {
        int index = findItemSlot(slots, item);
        Debug.Log("Index = " + index);
        if (index != -1)
        {
            slots[index].item.IncreaseAmount();
        } 
        else
        {
            ItemAddedToEmptySlot(slots, item);
        }
    }

    private void ItemAddedToEmptySlot(List<Slot> slots, Item item) 
    {
        int emptySlotIndex = FindEmptySlot(slots);
        if (emptySlotIndex == -1)
        {
            Debug.LogError("There isn't an empty slot in the inventory");
            return;
        }
        slots[emptySlotIndex].SetContent(item);
    }

    private int FindEmptySlot(List<Slot> slots)
    {
        int index = -1;
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].isEmpty())
            {
                index = i;
                break;
            }
        }
        return index;
    }

    private int findItemSlot(List<Slot> slots, Item item)
    {
        int index = -1;
        for (int i = 0; i < slots.Count; i++)
        {
            if(!slots[i].isEmpty())
                if (slots[i].item.item.itemName == item.itemName)
                {
                    if(slots[i].item.amount < item.stackSize) 
                    {
                        index = i;
                        break;
                    }
                }
        }
        return index;
    }

    private void OpenContainer(ContainerItem container)
    {
        isContainerOpen = true;
        containerPanel.gameObject.SetActive(isContainerOpen);
        InstantiateContainerSlots(container);

    }

    private void CloseContainerPanel()
    {
        isContainerOpen = false;
        containerPanel.gameObject.SetActive(isContainerOpen);
        ContainerController.Instance.CloseContainer();

        foreach(Transform slot in containerSlotPanel.transform)
        {
            Destroy(slot.gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            isInventoyOpen = !isInventoyOpen;
            inventoryPanel.gameObject.SetActive(isInventoyOpen);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isInventoyOpen = false;
            inventoryPanel.gameObject.SetActive(isInventoyOpen);
            CloseContainerPanel();
        }
    }
}