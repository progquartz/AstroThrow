using System.Collections.Generic;
using UnityEngine;

public class GravityZone : MonoBehaviour
{
    public const float TargetSpeed = 0.3f; // ��ǥ�� �ϴ� �ӵ�
    public const float GravityZoneRadius = 2.28f;
    public const float DragRatio = 0.5f;
    public static Vector2 CenterPos;
    [SerializeField] private List<Rigidbody2D> objectsInZone = new List<Rigidbody2D>(); // Ʈ���ŵ� ������Ʈ �����

    private void Awake()
    {
        CenterPos = transform.position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        if (rb != null && !objectsInZone.Contains(rb))
        {
            objectsInZone.Add(rb); // ����Ʈ�� ������Ʈ �߰�
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        if (rb != null && objectsInZone.Contains(rb))
        {
            RemoveObject(rb); // ����Ʈ���� ������Ʈ ����
        }
    }

    void FixedUpdate()
    {
        // �� �����Ӹ��� ����Ʈ�� �ִ� ������Ʈ���� ������
        foreach (Rigidbody2D rb in objectsInZone)
        {
            if (rb != null)
            {
                Vector2 direction = (Vector2)(transform.position - rb.transform.position); // ���� ���
                float distance = direction.magnitude; // �Ÿ� ���
                // �Ÿ��� ���� ���϶�� �������.
                if(distance < GravityZoneRadius)
                {
                    // ������ ���� ���� ����Ͽ� ������ �ӵ��� �޼�
                    Vector2 normalizedDirection = direction.normalized;
                    float requiredForce = rb.mass * TargetSpeed / Time.fixedDeltaTime;
                    Vector2 gravityForce = normalizedDirection * requiredForce;

                    Vector2 velocityDragDir = -rb.velocity.normalized;
                    Vector2 velocityDrag = velocityDragDir * gravityForce.magnitude * DragRatio;

                    Vector2 dynamicForce = velocityDrag + gravityForce;

                    rb.AddForce(dynamicForce);
                    //rb.AddForce(normalizedDirection * requiredForce); // ���� ���Ͽ� ���� �ӵ��� ������
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

    // ����Ʈ���� Ư�� ������Ʈ�� �����ϴ� �޼���
    public void RemoveObject(Rigidbody2D obj)
    {
        if (objectsInZone.Contains(obj))
        {
            objectsInZone.Remove(obj);
        }
    }
}
