using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class QuestLogManager : MonoBehaviour
{

    [SerializeField] private RectTransform inProgressQuestContainer;
    [SerializeField] private RectTransform turnedInQuestContainer;
    [SerializeField] private RectTransform questDetailContainer;
    [SerializeField] private RectTransform questObjectiveContainer;

    [SerializeField] private Text questTitle;
    [SerializeField] private Text questDescription;

    private List<QuestUI> inProgressQuestUIList;
    private List<QuestUI> turnedInQuestUIList;
    private QuestUI questUIprefab;
    private GoalTextUI goalTextUIprefab;
    private QuestUI selectedQuest;

    private bool isOpen = false;

    private void Start()
    {
        SetupEventListeners();
        inProgressQuestContainer.gameObject.SetActive(false);
        turnedInQuestContainer.gameObject.SetActive(false);
        questUIprefab = Resources.Load<QuestUI>("UI/QuestUI");
        goalTextUIprefab = Resources.Load<GoalTextUI>("UI/GoalText");
        inProgressQuestUIList = new List<QuestUI>();
        turnedInQuestUIList = new List<QuestUI>();
        this.transform.GetChild(0).gameObject.SetActive(isOpen);
        questDetailContainer.gameObject.SetActive(false);
    }

    private void SetupEventListeners()
    {
        UIEventHandler.onQuestAccepted += QuestAccepted;
        UIEventHandler.onQuestUpdated += QuestUpdated;
        UIEventHandler.onQuestTurnedIn += QuestTurnedIn;
        UIEventHandler.onQuestUISelected += QuestUISelected;
    }

    private void QuestAccepted(QuestTracker quest)
    {
        QuestUI questUI = Instantiate(questUIprefab, inProgressQuestContainer);
        questUI.SetQuest(quest);
        if(inProgressQuestUIList.Count == 0){
            inProgressQuestContainer.gameObject.SetActive(true);
            QuestUISelected(questUI);
        }
        inProgressQuestUIList.Add(questUI);
    }

    private void QuestUISelected(QuestUI questUI)
    {
        if (selectedQuest != null) selectedQuest.SetDeslected();
        questUI.SetSelected();
        selectedQuest = questUI;
        questTitle.text = questUI.questTracker.quest.name;
        questDescription.text = questUI.questTracker.quest.description;
        updateGoalText();;
        questDetailContainer.gameObject.SetActive(true);
        
    }

    private void QuestUpdated(QuestTracker quest)
    {
        if(quest == selectedQuest.questTracker)
        {
            updateGoalText();
        }
    }


    private void QuestTurnedIn(QuestTracker quest)
    {
        QuestUI questUI = inProgressQuestUIList.Find(q => q.questTracker == quest);
        questUI.transform.SetParent(turnedInQuestContainer);
        questUI.transform.SetSiblingIndex(0);
        inProgressQuestUIList.Remove(questUI);
        turnedInQuestUIList.Add(questUI);
        turnedInQuestContainer.gameObject.SetActive(true);
    }

    private void updateGoalText()
    {
        foreach (Transform child in questObjectiveContainer.transform) {
            Destroy(child.gameObject);
        }
        foreach(CollectionGoalTracker goalTracker in selectedQuest.questTracker.goalTrackers)
        {
            CollectionGoal goal = (CollectionGoal)goalTracker.goal;
            GoalTextUI goalText = Instantiate(goalTextUIprefab, questObjectiveContainer);
            goalText.SetGoalText(goal.itemSlug);
            goalText.SetProgress(goalTracker.currentAmount + "/" + goal.requiredAmount);

        }
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.K))
        {
            isOpen = !isOpen;
            this.transform.GetChild(0).gameObject.SetActive(isOpen);
        }
    }
}