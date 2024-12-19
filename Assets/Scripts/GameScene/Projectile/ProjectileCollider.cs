using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollider : MonoBehaviour
{
    [SerializeField] private IProjectile owner;
    [SerializeField] private Transform ownerTransform;
    private CircleCollider2D collider;

    private void Awake()
    {
        collider = GetComponent<CircleCollider2D>();
        owner = ownerTransform.GetComponent<IProjectile>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.name + "과 충돌");
        if (collision.gameObject.tag == "Planet")
        {
            if (!owner.GetActivationStatus())
            {
                Managers.Score.UpdateScore();
                owner.SetActivationStatus(true);
            }

            ProjectileCollider data;
            collision.TryGetComponent<ProjectileCollider>(out data);
            if(data != null)
            {
                int collisionRank = data.GetRank();
                if (GetRank() == collisionRank)
                {
                    owner.Merge(data.GetOwner() as IProjectile);
                }

            }
        }
    }

    [Obsolete("임시로 같은 코드 적용중.")]
    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.name + "과 충돌");
        if (collision.gameObject.tag == "Planet")
        {
            if (!owner.GetActivationStatus())
            {
                Managers.Score.UpdateScore();
                owner.SetActivationStatus(true);
            }
            int collisionRank = collision.gameObject.GetComponent<ProjectileCollider>().GetRank();
            if (GetRank() == collisionRank)
            {
                owner.Merge(collision.GetComponent<ProjectileCollider>().GetOwner() as IProjectile);
                //owner.Merge(collision.gameObject.GetComponent(typeof(IProjectile)) as IProjectile);
            }
        }
    }

    public IProjectile GetOwner()
    {
        return owner;
    }

    public void SetColliderRadius(float size)
    {
        collider.radius = size;
    }

    public int GetRank()
    {
        return owner.GetRank();
    }


}
