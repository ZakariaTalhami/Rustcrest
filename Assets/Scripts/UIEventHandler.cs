using UnityEngine;

public class UIEventHandler : MonoBehaviour
{
    public delegate void SimpleUIEventHandler();
    public delegate void ContainerEventHandler(ContainerItem container);
    public delegate void ItemEventHandler(Item item);
    public delegate void DialogEventHandler(NPC speaker);
    public static event ItemEventHandler onItemAddedToInventory;
    public static event ItemEventHandler onItemAddedToContainer;
    public static event ItemEventHandler onItemDroppedFromInventroy;
    public static event ContainerEventHandler onContainerOpened;
    public static event SimpleUIEventHandler onContainerClosed;
    public static event DialogEventHandler onDialogEnded;

    public static void ItemAddedToInventory(Item item)
    {
        if (onItemAddedToInventory != null)
            onItemAddedToInventory(item);
    }

    public static void ItemAddedToContainer(Item item)
    {
        onItemAddedToContainer?.Invoke(item);
    }

    public static void ItemDroppedFromInventory(Item item)
    {
        if (onItemDroppedFromInventroy != null)
            onItemDroppedFromInventroy(item);
    }

    public static void ContainerOpenned(ContainerItem container)
    {
        onContainerOpened?.Invoke(container);
    }

    public static void ContainerClosed()
    {
        onContainerClosed?.Invoke();
    }

    public static void DialogEnded(NPC speaker){
        onDialogEnded?.Invoke(speaker);
    }
}