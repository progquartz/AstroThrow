using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAstro : MonoBehaviour, IProjectile
{
    public AstroDataSO AstroDataSO;
    private AstroVisual visual;
    private Rigidbody2D rb;
    
    [SerializeField] private CircleCollider2D collisionCollider;
    [SerializeField] private CircleCollider2D triggerCollider;

    [SerializeField] private Transform triggerTransform;
    

    [SerializeField] private bool isProjectileActive;

    private void InitializeData()
    {
        rb = GetComponent<Rigidbody2D>();
        visual = GetComponent<AstroVisual>();
        collisionCollider = GetComponent<CircleCollider2D>();
        triggerCollider = triggerTransform.GetComponent<CircleCollider2D>();
    }


    public void SetData(AstroDataSO data)
    {
        AstroDataSO = data;
        gameObject.name = data.Name;
        rb.mass = data.Mass;
        collisionCollider.radius = data.CollisionSize;
        triggerCollider.radius = data.TriggerSize;
    }

    public bool Merge(IProjectile target)
    {
        // ����� �������� �����ϱ�.
        Vector2 centerPos = (GetCenterPos() + target.GetCenterPos())/2;

        // ����� �����, �ڽ��� �� ��ġ�� ���� ���� �����ͷ� ��ȭ.
        ProjectileManager.instance.RemoveFromList(target);
        
        target.DestroySelf();

        RefreshAstro(AstroDataSO.NextAstro);
        transform.position = centerPos;
        return false;
    }

    public void RefreshAstro(AstroDataSO newAstro)
    {
        // ������Ʈ �߰�.
        InitializeData();
        AstroDataSO = newAstro;
        if(newAstro != null)
        {
            SetData(AstroDataSO);
            visual.SetVisual(AstroDataSO);
        }
        else
        {
            DestroySelf();
        }
    }

    public bool DestroySelf()
    {
        Debug.Log(gameObject.name + "�� ������ ȣ��Ǿ���.");
        if(ProjectileManager.instance.RemoveFromList(this))
        {
            Destroy(this.gameObject);
            return true;
        }
        else
        {
            Destroy(this.gameObject);
            return false;
        }
    }


    public int GetRank()
    {
        return AstroDataSO.Rank;
    }

    public Vector2 GetCenterPos()
    {
        return transform.position;
    }


    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public bool CheckAvailability()
    {
        return isProjectileActive;
    }

    public void SetActivationStatus(bool isActive)
    {
        isProjectileActive = isActive;
    }

    public bool GetActivationStatus()
    {
        return isProjectileActive;
    }

    public float GetRadius()
    {
        return AstroDataSO.TriggerSize;
    }

    public int GetScore()
    {
        return AstroDataSO.Score;
    }
}
