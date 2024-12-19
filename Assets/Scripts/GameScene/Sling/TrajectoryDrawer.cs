using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrajectoryDrawer : MonoBehaviour
{
    public int simulationSteps = 100; // 시뮬레이션 스텝 수
    public LineRenderer lineRenderer; // 궤적을 그릴 라인 렌더러
    public float timeStep = 0.01f; // 시뮬레이션 시간 간격
    
    [SerializeField] private Transform gravityZoneCenter;

    public void DrawTrajectory(Vector3 initialPosition, Vector3 initialVelocity, Rigidbody2D throwingObject)
    {
        //Debug.Log("호출됨");
        lineRenderer.enabled = true;
        Vector3[] position = new Vector3[simulationSteps];

        float mass = throwingObject.mass;
        Vector3 framePosition = initialPosition;
        Vector3 frameVelocity = initialVelocity;
        position[0] = framePosition;

        // 시뮬레이션을 통한 궤적 계산
        for (int i = 1; i < simulationSteps; i++)
        {
            // 시뮬레이션 스텝마다 가상의 물리 업데이트
            (framePosition, frameVelocity) = SimulateStep(framePosition, frameVelocity, mass);
            position[i] = framePosition;
        }

        // 궤적을 라인 렌더러에 표시
        lineRenderer.positionCount = position.Length;
        lineRenderer.SetPositions(position);

    }

    public void EraseTrajectory()
    {
        lineRenderer.enabled = false;
    }

    private (Vector3,Vector3) SimulateStep(Vector3 preFramePosition, Vector3 preFrameVelocity, float simulatingMass)
    {
        // 외력을 반영하여 투사체의 속도 및 위치 업데이트
        Vector2 forces = GetDynamicForces(preFramePosition, preFrameVelocity, simulatingMass); // 다양한 외력 계산
        Vector3 acceleration = forces / simulatingMass; // 가속도 = 힘 / 질량

        Vector3 newVelocity = preFrameVelocity + acceleration * timeStep;
        Vector3 newPosition = preFramePosition + newVelocity * timeStep;

        //Debug.Log("오브젝트를 " + newVelocity * timeStep + "만큼 움직이게 만들었습니다.");
        return (newPosition, newVelocity);
    }

    Vector2 GetDynamicForces(Vector2 prevPosition, Vector2 prevVelocity, float simulatingMass)
    {
        float gravityAreaRadius = GravityZone.GravityZoneRadius;
        // 중력장 중심에서 투사체로 가는 방향을 계산
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

            // 예시로 중력과 바람을 적용
            return gravityForce;
        }
        else
        {
            return Vector2.zero;
        }
    }

}
