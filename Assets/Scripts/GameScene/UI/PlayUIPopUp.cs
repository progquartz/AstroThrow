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

    // 이거 데이터 직렬화해서 dataManager쪽으로 넘기기.
    private string scoreTextLore = "Score : ";



    // 이거 비업데이트 기반으로 제작.
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
