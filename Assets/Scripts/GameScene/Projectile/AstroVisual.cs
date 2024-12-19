using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroVisual : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private EyeController eyeController;

    public void SetVisual(AstroDataSO data)
    {
        spriteRenderer.sprite = data.Sprite;
        eyeController.SetEyesTransform(data);
    }
}
