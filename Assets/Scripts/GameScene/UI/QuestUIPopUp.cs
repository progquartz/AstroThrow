using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestUIPopUp : UIPopUp
{

    [Header("Quest Board UI")]

    [SerializeField] private RectTransform[] questPortionTransforms;

    private Image[] questImages;
    private TextMeshProUGUI[] questCounters;

    private TextMeshProUGUI[] questCompleteButtonTexts;
    private Button[] questCompleteButtons;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        // 데이터 입력
        for (int i = 0; i < questPortionTransforms.Length; i++)
        {
            questImages[i] = questPortionTransforms[i].Find("QuestTargetImage").GetComponent<Image>();
            questCounters[i] = questPortionTransforms[i].Find("QuestTargetCounter").GetComponent<TextMeshProUGUI>();
            questCompleteButtonTexts[i] = questPortionTransforms[i].Find("QuestRewardText").GetComponent<TextMeshProUGUI>();
            questCompleteButtons[i] = questPortionTransforms[i].Find("QuestRewardButton").GetComponent<Button>();
        }

        return true;
    }

    public void TurnOffQuestUIPopUp()
    {
        Managers.UI.ClosePopupUI(this);
    }
    
}
