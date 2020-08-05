using System;
using UnityEngine;

[Serializable]
public class CollectionGoalTracker : GoalTracker
{
    public int currentAmount = 0;

    public CollectionGoalTracker(QuestTracker quest, BaseGoal goal) : base(quest, goal)
    {
        this.currentAmount = InventoryController.instance.GetItemCount(((CollectionGoal)goal).itemSlug);
        Evaluate();
    }

    public void TurnInGoalItems()
    {
        CollectionGoal collectionGoal = (CollectionGoal)goal;
        Item goalItem = ItemDatabase.instance.getItem(collectionGoal.itemSlug);
        MethodHelpers.RepeatAction(
            collectionGoal.requiredAmount,
            () => { InventoryController.instance.OfferItem(goalItem); }
        );
    }

    protected override void SetEventListeners()
    {
        UIEventHandler.onItemAddedToInventory += ItemAddedOrRemovedToInventroy;
        UIEventHandler.onItemDroppedFromInventory += ItemAddedOrRemovedToInventroy;
    }

    protected override void RemoveEventListners()
    {
        UIEventHandler.onItemAddedToInventory -= ItemAddedOrRemovedToInventroy;
        UIEventHandler.onItemDroppedFromInventory -= ItemAddedOrRemovedToInventroy;
    }

    public override void CompleteGoal()
    {
        base.CompleteGoal();
        TurnInGoalItems();
    }

    protected override bool CheckConditions()
    {
        if (currentAmount < ((CollectionGoal)goal).requiredAmount)
        {
            return false;
        }

        return true;
    }

    private void ItemAddedOrRemovedToInventroy(Item item)
    {
        CollectionGoal collectionGoal = (CollectionGoal)goal;
        if (item.slugName == ((CollectionGoal)goal).itemSlug)
        {
            int inventoryCount = InventoryController.instance.GetItemCount(collectionGoal.itemSlug);
            Debug.LogWarning(inventoryCount);
            currentAmount = Math.Min(Math.Max(inventoryCount, 0), collectionGoal.requiredAmount);
            Evaluate();
        }
    }

}