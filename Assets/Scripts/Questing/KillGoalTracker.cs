using System;

[Serializable]
public class KillGoalTracker : GoalTracker{
    public int currentAmount = 0;

    public KillGoalTracker(QuestTracker quest, BaseGoal goal) : base(quest, goal){}

    protected override bool CheckConditions()
    {
        if(currentAmount < ((CollectionGoal)goal).requiredAmount)
        {
            return false;
        }

        return true;
    }
}