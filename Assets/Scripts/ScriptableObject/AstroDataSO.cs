using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName ="AstroDataSO", menuName = "ScriptableObject/AstroDataSO")]
public class AstroDataSO : ScriptableObject
{
    public string Name;
    public int Rank;
    public int Score;
    public float Mass;
    public float CollisionSize;
    public float TriggerSize;
    public Sprite Sprite;
    public AstroDataSO NextAstro;
    public Vector2 LeftEyePos;
    public Vector2 RightEyePos;
    public Vector2 EyeScale;
}
