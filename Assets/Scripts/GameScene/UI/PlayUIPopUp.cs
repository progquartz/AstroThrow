using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayUIPopUp : UIBase
{
    [Header("Next Astro UI")]
    [SerializeField] private Image nextAstroSprite;


    [Header("Text Scoring UI")]
    [SerializeField] private TextMeshProUGUI scoreText;

    // �̰� ������ ����ȭ�ؼ� dataManager������ �ѱ��.
    private string scoreTextLore = "Score : ";



    // �̰� �������Ʈ ������� ����.
    private void Update()
    {
        UpdateNextAstroSprite();
        UpdateScoringText();
    }

    public void UpdateNextAstroSprite()
    {
        nextAstroSprite.sprite = ProjectileManager.instance.GetNextProjectileSO().Sprite;
    }
    
    public void UpdateScoringText()
    {
        scoreText.text = scoreTextLore + Managers.Score.Score.ToString();
    }
}
