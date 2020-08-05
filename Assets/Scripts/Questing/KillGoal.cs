using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(fileName = "KillGoal", menuName = "Rustcrest/Goal/KillGoal", order = 0)]
public class KillGoal : BaseGoal
{
    public int enemyID;
    public int requiredAmount;

}