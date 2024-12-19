using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectile
{
    public Vector2 GetCenterPos();

    public bool Merge(IProjectile target);

    public GameObject GetGameObject();

    public int GetRank();
    public int GetScore();

    public void SetActivationStatus(bool isActive);
    public bool GetActivationStatus();
    public float GetRadius();

    public bool CheckAvailability();

    public bool DestroySelf();

}
