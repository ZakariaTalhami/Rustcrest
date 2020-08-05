using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Quest", menuName = "Rustcrest/Quest", order = 0)]
public class Quest : ScriptableObject
{
    public new string name;
    public string description;
    public List<BaseGoal> goals;

    public Dialog questDialog;
    public Dialog inProgressDialog;
    public Dialog completeDialog;

}