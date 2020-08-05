using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;


[Serializable]
public class QuestTracker
{
    public enum QuestState
    {
        Pending, InProgress, Complete, TurnedIn
    }
    public Quest quest;
    public List<GoalTracker> goalTrackers;
    public QuestState state;

    public QuestTracker(Quest quest, QuestState state)
    {
        this.quest = quest;
        this.state = state;
        
        // Create the goal Trackers 
        goalTrackers = new List<GoalTracker>();
        GoalTrackerFactory factory = new GoalTrackerFactory();
        foreach (BaseGoal goal in this.quest.goals)
        {
            goalTrackers.Add((GoalTracker)factory.GetGoalTracker(goal, this));
        }
    }

    public void evaluate()
    {

        // if(goalTrackers.Count > 0 && goalTrackers.All(tracker => tracker.isCompleted))
        if(goalTrackers.All(tracker => tracker.isCompleted))
        {
            Debug.Log("SetAsComplete");
            SetAsComplete();
        }
        else
        {
            Debug.Log("SetAsInProgress");
            SetAsInProgress();
        }
    }

    public void SetAsInProgress()
    {
        state = QuestState.InProgress;
    }

    public void SetAsComplete()
    {
        state = QuestState.Complete;
    }

    public void SetAsTurnIn()
    {
        state = QuestState.TurnedIn;
        goalTrackers.ForEach(tracker => tracker.CompleteGoal());
    }
}