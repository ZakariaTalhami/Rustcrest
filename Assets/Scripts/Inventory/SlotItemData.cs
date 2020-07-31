using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotItemData : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public bool isInventoryItem;
    public Item item;
    public int amount;

    public Image icon;
    public Text amountText;

    private Transform originalParent;

    public void SetItem(Item item)
    {
        this.item = item;
        this.amount = 1;
        icon.sprite = item.icon;
        amountText.text = amount.ToString();
    }

    public void SetItem(Item item, int itemAmount)
    {
        this.item = item;
        this.amount = itemAmount;
        icon.sprite = item.icon;
        amountText.text = amount.ToString();
    }

    public void IncreaseAmount()
    {
        this.IncreaseAmount(1);
    }

    public void IncreaseAmount(int increaseAmount)
    {
        amount += increaseAmount;
        amountText.text = amount.ToString();
    }

    public void DecreaseAmount()
    {
        this.DecreaseAmount(1);
    }

    public void DecreaseAmount(int decreaseAmount)
    {
        amount -= decreaseAmount;
        amountText.text = amount.ToString();
    }

    public void remove()
    {
        Destroy(gameObject);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            originalParent = this.transform.parent;
            this.transform.SetParent(this.transform.parent.parent.parent.parent);
            // Disable Raycast target to for when dropped
            icon.raycastTarget = false;
            this.transform.position = eventData.position;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            this.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(EventSystem.current.IsPointerOverGameObject() || !isInventoryItem) 
        {
            this.transform.SetParent(originalParent);
            this.transform.localPosition = Vector3.zero;
            icon.raycastTarget = true;
        } 
        else
        {
            if(InventoryController.instance.isItemDropable(this.item))
            {
                for (int i = 0; i < amount; i++)
                {
                    InventoryController.instance.dropItem(this.item);
                }
                remove();
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            if(this.isInventoryItem)
            {
                if(ContainerController.Instance.isContainerOpen)
                {
                    // Deposit item in the container
                    for (int i = 0; i < amount; i++)
                    {
                        ContainerController.Instance.Deposit(this.item);
                        InventoryController.instance.RemoveItem(this.item);
                    }
                    Destroy(gameObject);
                }
            }
            else
            {
                // withdraw iten into container
                for (int i = 0; i < amount; i++)
                {
                    InventoryController.instance.GiveItem(this.item);
                    ContainerController.Instance.Withdraw(this.item);
                }
                Destroy(gameObject);
            }
            
        }
    }

}
