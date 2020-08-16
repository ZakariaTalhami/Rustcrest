using UnityEngine;
using UnityEngine.UI;

public class GoalTextUI : MonoBehaviour {
    
    public BaseGoal goal;
    
    [SerializeField]
    private Text goalText;
    
    [SerializeField]
    private Text progressText;

    public void SetUpGoal(BaseGoal goal)
    {
        this.goal = goal;
    }

    public void SetGoalText(string goalText)
    {
        this.goalText.text = goalText;
    }

    public void SetProgress(string progress)
    {
        this.progressText.text = progress;
    }

}