using UnityEngine;

public class NPC : Interactable {
    
    public Dialog dialog;
    public new string name;

    public override void Interacte()
    {
        // Open Dialog 
        DialogController.Instance.SetDialogInteraction(this, dialog);
    }
}