using UnityEngine;

public class QuestGiverMarkerHandler : MonoBehaviour {
    
    public GameObject exclamationMark;
    public GameObject questionMark;

    private void Start() {
        HideAllMarks();
    }

    public void ShowExclamationMark()
    {
        exclamationMark.SetActive(true);
        questionMark.SetActive(false);
    }

    public void ShowQuestionMark()
    {
        exclamationMark.SetActive(false);
        questionMark.SetActive(true);
    }

    public void HideAllMarks()
    {
        exclamationMark.SetActive(false);
        questionMark.SetActive(false);
    }
}