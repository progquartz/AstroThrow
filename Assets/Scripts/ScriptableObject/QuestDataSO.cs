using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "QuestDataSO", menuName = "ScriptableObject/QuestDataSO")]
public class QuestDataSO : ScriptableObject
{
    public List<QuestTarget> targetData;
    public QuestReward rewardData;
}

[System.Serializable]
public class QuestReward
{
    public int goldReward;
    public int jewelReward;
}

[System.Serializable]
public class QuestTarget
{
    public AstroDataSO targetAstro;
    public int targetCount;
}
