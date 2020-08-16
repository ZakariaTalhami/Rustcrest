using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour {
    
    [SerializeField] private Text textUI;
    [SerializeField] private Button buttonUI;
    [SerializeField] private Image imageUI;
    [SerializeField][Range(0, 255)] private int selectedAlpha;
    [SerializeField][Range(0, 255)] private int deselectedAlpha;
    public QuestTracker questTracker;

    
    private void Start() {
        buttonUI.onClick.AddListener(() => QuestUIClicked());
    }

    public void SetQuest(QuestTracker questTracker)
    {
        this.questTracker = questTracker;
        textUI.text = questTracker.quest.name;
    }

    public void UpdateQuestInformation(QuestTracker questTracker)
    {
        if(this.questTracker == questTracker)
        {
            textUI.text = questTracker.quest.name;
        }
    }

    public void SetSelected()
    {
        Debug.Log("Selected");
        Color imageColor = imageUI.color;
        imageColor.a = (float) selectedAlpha / 255;
        imageUI.color = imageColor;
    }

    public void SetDeslected()
    {
        Debug.Log("Deseleted");
        Color imageColor = imageUI.color;
        imageColor.a = (float) deselectedAlpha / 255;
        imageUI.color = imageColor;
    }

    private void QuestUIClicked()
    {
        Debug.Log(questTracker.quest.name + " Clicked!");
        UIEventHandler.QuestUISelected(this);
    }
}