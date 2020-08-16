using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{

    public RectTransform dialogPanel;
    private Text dialogTextUI;
    private Text nameTextUI;
    private Button continueButtonUI;


    private NPC speaker;
    private Dialog currentDialog;
    private int dialogIndex = -1;

    #region Singleton
    public static DialogController Instance;
    private void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            GetUIElements();
            dialogPanel.gameObject.SetActive(false);
            UIEventHandler.onDialogInterrupted += CloseDialogInteraction;
        }
    }

    #endregion Singleton

    private void GetUIElements()
    {
        dialogTextUI = dialogPanel.Find("DialogText").GetComponent<Text>();
        continueButtonUI = dialogPanel.Find("ContinueText").GetComponent<Button>();
        nameTextUI = dialogPanel.Find("Name").GetChild(0).GetComponent<Text>();
        continueButtonUI.onClick.AddListener(() => SetNextDialogText());
    }

    public void SetDialogInteraction(NPC speaker, Dialog dialog)
    {
        this.currentDialog = dialog;
        this.speaker = speaker;
        SetNextDialogText();
        this.nameTextUI.text = speaker.name;
        this.dialogPanel.gameObject.SetActive(true);
    }

    public void CloseDialogInteraction()
    {
        this.currentDialog = null;
        this.speaker = null;
        this.dialogIndex = -1;
        this.dialogPanel.gameObject.SetActive(false);
    }

    private void SetNextDialogText()
    {
        if (dialogIndex < currentDialog.text.Count - 1)
        {
            dialogIndex++;
            dialogTextUI.text = currentDialog.text[dialogIndex];
        }
        else
        {
            UIEventHandler.DialogEnded(speaker);
            CloseDialogInteraction();
        }
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if(dialogPanel.gameObject.activeInHierarchy == true)
            {
                SetNextDialogText();
            }
        }
    }
}