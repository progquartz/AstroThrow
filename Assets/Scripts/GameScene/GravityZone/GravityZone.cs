using System.Collections.Generic;
using UnityEngine;

public class GravityZone : MonoBehaviour
{
    public const float TargetSpeed = 0.3f; // 목표로 하는 속도
    public const float GravityZoneRadius = 2.28f;
    public const float DragRatio = 0.5f;
    public static Vector2 CenterPos;
    [SerializeField] private List<Rigidbody2D> objectsInZone = new List<Rigidbody2D>(); // 트리거된 오브젝트 저장소

    private void Awake()
    {
        CenterPos = transform.position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        if (rb != null && !objectsInZone.Contains(rb))
        {
            objectsInZone.Add(rb); // 리스트에 오브젝트 추가
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        if (rb != null && objectsInZone.Contains(rb))
        {
            RemoveObject(rb); // 리스트에서 오브젝트 제거
        }
    }

    void FixedUpdate()
    {
        // 매 프레임마다 리스트에 있는 오브젝트들을 끌어당김
        foreach (Rigidbody2D rb in objectsInZone)
        {
            if (rb != null)
            {
                Vector2 direction = (Vector2)(transform.position - rb.transform.position); // 방향 계산
                float distance = direction.magnitude; // 거리 계산
                // 거리가 일정 이하라면 끌어오기.
                if(distance < GravityZoneRadius)
                {
                    // 질량에 따른 힘을 계산하여 일정한 속도를 달성
                    Vector2 normalizedDirection = direction.normalized;
                    float requiredForce = rb.mass * TargetSpeed / Time.fixedDeltaTime;
                    Vector2 gravityForce = normalizedDirection * requiredForce;

                    Vector2 velocityDragDir = -rb.velocity.normalized;
                    Vector2 velocityDrag = velocityDragDir * gravityForce.magnitude * DragRatio;

                    Vector2 dynamicForce = velocityDrag + gravityForce;

                    rb.AddForce(dynamicForce);
                    //rb.AddForce(normalizedDirection * requiredForce); // 힘을 가하여 일정 속도로 끌어당김
                }
            }
        }
    }
    /*

    public Vector2 GetDynamicForce(Vector2 prevPosition, Vector2 prevVelocity, float simulatingMass)
    {
        Vector2 
    }
    */

    // 리스트에서 특정 오브젝트를 제거하는 메서드
    public void RemoveObject(Rigidbody2D obj)
    {
        if (objectsInZone.Contains(obj))
        {
            objectsInZone.Remove(obj);
        }
    }
}
