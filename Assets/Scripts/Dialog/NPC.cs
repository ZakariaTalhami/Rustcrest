using UnityEngine;

public class NPC : Interactable {
    
    public Dialog dialog;
    public new string name;

    private void Start() {
        Init();
    }

    protected virtual void Init()
    {
        UIEventHandler.onDialogEnded += DialogInteractionEnded;
    }

    protected virtual Dialog GetDialog()
    {
        return dialog;
    }

    protected virtual void OpenDialog()
    {
        DialogController.Instance.SetDialogInteraction(this, GetDialog());
    }

    public override void Interacte()
    {
        OpenDialog();
    }

    private void DialogInteractionEnded(NPC speaker)
    {
        if(speaker.name == this.name)
        {
            PostDialogAction();
        }
    }

    protected virtual void PostDialogAction()
    {
        Debug.Log("Post Dialog Action for NPC");
    }
}