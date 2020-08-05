using System;
using UnityEngine;

public class GoalTrackerFactory {
    
    public GoalTracker GetGoalTracker(BaseGoal goal, QuestTracker quest)
    {
        Type goalType = goal.GetType();
        Debug.Log(goalType);
        if(goalType == typeof(CollectionGoal))
        {
            Debug.Log("Creating a CollectionGoalTracker");
            return new CollectionGoalTracker(quest, goal);
        } else if(goalType == typeof(KillGoal)) 
        {
            Debug.Log("Creating a KillGoalTracker");
            return new KillGoalTracker(quest, goal);
        }

        return null;
    }
}