using UnityEngine;

public class UIEventHandler : MonoBehaviour
{
    public delegate void SimpleUIEventHandler();
    public delegate void ContainerEventHandler(ContainerItem container);
    public delegate void ItemEventHandler(Item item);
    public delegate void DialogEventHandler(NPC speaker);
    public delegate void QuestTrackerEventHandler(QuestTracker tracker);
    public delegate void QuestUIEventHandler(QuestUI questUI);

    public static event ItemEventHandler onItemAddedToInventory;
    public static event ItemEventHandler onItemAddedToContainer;
    public static event ItemEventHandler onItemRemovedFromInventroy;
    public static event ItemEventHandler onItemDroppedFromInventory;
    public static event ContainerEventHandler onContainerOpened;
    public static event SimpleUIEventHandler onContainerClosed;
    public static event DialogEventHandler onDialogEnded;
    public static event SimpleUIEventHandler onDialogInterrupted;
    public static event QuestTrackerEventHandler onQuestAccepted;
    public static event QuestTrackerEventHandler onQuestUpdated;
    public static event QuestTrackerEventHandler onQuestTurnedIn;
    public static event QuestUIEventHandler onQuestUISelected;

    public static void ItemAddedToInventory(Item item)
    {
        if (onItemAddedToInventory != null)
            onItemAddedToInventory(item);
    }

    public static void ItemAddedToContainer(Item item)
    {
        onItemAddedToContainer?.Invoke(item);
    }

    public static void ItemRemovedFromInventory(Item item)
    {
        if (onItemRemovedFromInventroy != null)
            onItemRemovedFromInventroy(item);
    }

    public static void ItemDroppedFromInventory(Item item)
    {
        if (onItemDroppedFromInventory != null)
            onItemDroppedFromInventory(item);
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

    public static void DialogInterrupted(){
        onDialogInterrupted?.Invoke();
    }

    public static void QuestAccepted(QuestTracker tracker)
    {
        onQuestAccepted?.Invoke(tracker);
    }

    public static void QuestUpdated(QuestTracker tracker)
    {
        onQuestUpdated?.Invoke(tracker);
    }

    public static void QuestTurnedIn(QuestTracker tracker)
    {
        onQuestTurnedIn?.Invoke(tracker);
    }

    public static void QuestUISelected(QuestUI questUI)
    {
        onQuestUISelected?.Invoke(questUI);
    }
}