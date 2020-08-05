using UnityEngine;
using System.Collections.Generic;

public class QuestGiver : NPC
{
    public List<Quest> quests;
    public QuestGiverMarkerHandler markerHandler;

    protected override void Init()
    {
        base.Init();
        markerHandler = transform.Find("QuestGiverMarkers").GetComponent<QuestGiverMarkerHandler>();
        if (markerHandler != null)
        {
            if (HasQuest()) markerHandler.ShowExclamationMark();
        }
    }

    protected override Dialog GetDialog()
    {
        Dialog currentDialog = dialog;
        QuestController questController = QuestController.Instance;
        if (HasQuest())
        {
            if (questController.hasQuest(quests[0]))
            {
                if (questController.GetQuestState(quests[0]) == QuestTracker.QuestState.InProgress)
                {
                    currentDialog = quests[0].inProgressDialog;
                }
                else if (questController.GetQuestState(quests[0]) == QuestTracker.QuestState.Complete)
                {
                    currentDialog = quests[0].completeDialog;
                }
            }
            else
            {
                currentDialog = quests[0].questDialog;
            }
        }

        return currentDialog;
    }

    protected override void PostDialogAction()
    {
        QuestController questController = QuestController.Instance;
        if (HasQuest())
        {
            if (questController.hasQuest(quests[0]))
            {
                if (questController.GetQuestState(quests[0]) == QuestTracker.QuestState.Complete)
                {
                    QuestController.Instance.TurnInQuest(quests[0]);
                    quests.RemoveAt(0);
                    if(HasQuest())
                    {
                        markerHandler.ShowExclamationMark();
                    }
                    else
                    {
                        markerHandler.HideAllMarks();
                    }
                }
            }
            else
            {
                QuestController.Instance.GiveQuest(quests[0]);
                if (markerHandler != null)
                {
                    Debug.LogWarning("markerHandler.ShowQuestionMark();");
                    markerHandler.ShowQuestionMark();
                } 
            }
        }
    }

    private bool HasQuest()
    {
        return quests != null && quests.Count > 0;
    }
}