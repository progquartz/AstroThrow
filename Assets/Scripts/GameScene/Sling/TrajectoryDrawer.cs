using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrajectoryDrawer : MonoBehaviour
{
    public int simulationSteps = 100; // �ùķ��̼� ���� ��
    public LineRenderer lineRenderer; // ������ �׸� ���� ������
    public float timeStep = 0.01f; // �ùķ��̼� �ð� ����
    
    [SerializeField] private Transform gravityZoneCenter;

    public void DrawTrajectory(Vector3 initialPosition, Vector3 initialVelocity, Rigidbody2D throwingObject)
    {
        //Debug.Log("ȣ���");
        lineRenderer.enabled = true;
        Vector3[] position = new Vector3[simulationSteps];

        float mass = throwingObject.mass;
        Vector3 framePosition = initialPosition;
        Vector3 frameVelocity = initialVelocity;
        position[0] = framePosition;

        // �ùķ��̼��� ���� ���� ���
        for (int i = 1; i < simulationSteps; i++)
        {
            // �ùķ��̼� ���ܸ��� ������ ���� ������Ʈ
            (framePosition, frameVelocity) = SimulateStep(framePosition, frameVelocity, mass);
            position[i] = framePosition;
        }

        // ������ ���� �������� ǥ��
        lineRenderer.positionCount = position.Length;
        lineRenderer.SetPositions(position);

    }

    public void EraseTrajectory()
    {
        lineRenderer.enabled = false;
    }

    private (Vector3,Vector3) SimulateStep(Vector3 preFramePosition, Vector3 preFrameVelocity, float simulatingMass)
    {
        // �ܷ��� �ݿ��Ͽ� ����ü�� �ӵ� �� ��ġ ������Ʈ
        Vector2 forces = GetDynamicForces(preFramePosition, preFrameVelocity, simulatingMass); // �پ��� �ܷ� ���
        Vector3 acceleration = forces / simulatingMass; // ���ӵ� = �� / ����

        Vector3 newVelocity = preFrameVelocity + acceleration * timeStep;
        Vector3 newPosition = preFramePosition + newVelocity * timeStep;

        //Debug.Log("������Ʈ�� " + newVelocity * timeStep + "��ŭ �����̰� ��������ϴ�.");
        return (newPosition, newVelocity);
    }

    Vector2 GetDynamicForces(Vector2 prevPosition, Vector2 prevVelocity, float simulatingMass)
    {
        float gravityAreaRadius = GravityZone.GravityZoneRadius;
        // �߷��� �߽ɿ��� ����ü�� ���� ������ ���
        Vector2 directionToCenter = (Vector2)gravityZoneCenter.position - prevPosition;
        float distance = directionToCenter.magnitude;
        if (distance < gravityAreaRadius)
        {

            Vector2 gravityForce = directionToCenter.normalized * simulatingMass * GravityZone.TargetSpeed / Time.fixedDeltaTime;
            float gravityForceMag = gravityForce.magnitude;
            Vector2 velocityDragDir = -prevVelocity.normalized;
            Vector2 velocityDrag = velocityDragDir * gravityForceMag * GravityZone.DragRatio;


            Vector2 dynamicForce = velocityDrag + gravityForce;

            return dynamicForce;

            // ���÷� �߷°� �ٶ��� ����
            return gravityForce;
        }
        else
        {
            return Vector2.zero;
        }
    }

}
