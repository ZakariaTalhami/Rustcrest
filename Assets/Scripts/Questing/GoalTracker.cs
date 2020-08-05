using UnityEngine;
using System;

[Serializable]
public class GoalTracker
{

    public BaseGoal goal;

    [HideInInspector]
    public QuestTracker quest;
    public bool isCompleted = false;

    public GoalTracker(QuestTracker quest, BaseGoal goal)
    {
        this.quest = quest;
        this.goal = goal;
        SetEventListeners();
    }

    protected virtual bool CheckConditions()
    {
        return false;
    }

    public virtual void Evaluate()
    {
        isCompleted = CheckConditions();
        quest.evaluate();
    }

    protected virtual void SetEventListeners() { }
    protected virtual void RemoveEventListners() { }
    public virtual void CompleteGoal() {
        RemoveEventListners();
    }
}