using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeController : MonoBehaviour
{
    [SerializeField] private Transform leftEyeOuterTransform;
    [SerializeField] private Transform rightEyeOuterTransform;

    [SerializeField] private Transform leftEyeInnerTransform;
    [SerializeField] private Transform rightEyeInnerTransform;

    private float maxDistance = 0.06f;

    void Update()
    {
        MoveWithLimit(leftEyeInnerTransform, leftEyeOuterTransform);
        MoveWithLimit(rightEyeInnerTransform, rightEyeOuterTransform);
    }

    void MoveWithLimit(Transform innerTransform, Transform outerTransform)
    {
        Vector2 distanceFromCenter = (Vector2)innerTransform.position - (Vector2)outerTransform.position;
        float distance = distanceFromCenter.magnitude;
        float scaleMaxDistance = maxDistance * leftEyeOuterTransform.localScale.x;

        if (distance > scaleMaxDistance)
        {
            Vector2 clampedPosition = (Vector2)outerTransform.position + distanceFromCenter.normalized * scaleMaxDistance;
            innerTransform.position = clampedPosition;
        }
    }

    public void SetEyesTransform(AstroDataSO data)
    {
        leftEyeOuterTransform.localPosition = data.LeftEyePos;
        rightEyeOuterTransform.localPosition = data.RightEyePos;
        leftEyeOuterTransform.localScale = data.EyeScale;
        rightEyeOuterTransform.localScale = data.EyeScale;
    }


}
