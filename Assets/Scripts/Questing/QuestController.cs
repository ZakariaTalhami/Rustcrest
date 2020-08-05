using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class QuestController : MonoSingleton<QuestController>
{

    public List<QuestTracker> questTrackers = new List<QuestTracker>();

    protected override void init()
    {
        // GiveQuest(Resources.Load<Quest>("Quests/LogQuest"));
    }

    public bool hasQuest(Quest quest)
    {
        return questTrackers.Any(tracker => tracker.quest == quest);
    }

    public QuestTracker GetQuestTracker(Quest quest)
    {
        return questTrackers.Single(tracker => tracker.quest == quest);
    }

    public QuestTracker.QuestState GetQuestState(Quest quest)
    {
        return GetQuestTracker(quest).state; 
    }


    public void GiveQuest(Quest quest)
    {
        questTrackers.Add(new QuestTracker(quest, QuestTracker.QuestState.InProgress));
    }

    public void TurnInQuest(Quest quest)
    {
        QuestTracker tracker = GetQuestTracker(quest);
        tracker.SetAsTurnIn();
    }

}