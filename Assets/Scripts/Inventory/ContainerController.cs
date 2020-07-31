using UnityEngine;

public class ContainerController : MonoBehaviour
{

    public static ContainerController Instance;


    private ContainerItem currentContainerItem;
    public bool isContainerOpen = false;
    private void Start()
    {
        if (Instance != null && Instance != this) 
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void OpenContainer(ContainerItem container)
    {
        currentContainerItem = container;
        isContainerOpen = true;
        UIEventHandler.ContainerOpenned(container);
    }

    public void CloseContainer()
    {
        isContainerOpen = false;
        this.currentContainerItem = null;
    }

    public bool Withdraw(Item item)
    {
        return currentContainerItem.removeItem(item);
    }

    public bool Deposit(Item item)
    {
        if(currentContainerItem.additem(item))
        {
            UIEventHandler.ItemAddedToContainer(item);
            return true;
        }
        return false;
    }

    public bool DepositWithoutUI(Item item)
    {
        return currentContainerItem.additem(item);
    }
}