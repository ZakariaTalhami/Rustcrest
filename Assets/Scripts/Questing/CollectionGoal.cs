using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(fileName = "CollectionGoal", menuName = "Rustcrest/Goal/CollectionGoal", order = 0)]
public class CollectionGoal : BaseGoal
{
    public string itemSlug;
    public int requiredAmount;

}