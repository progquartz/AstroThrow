using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityZoneVisual : MonoBehaviour
{
    [SerializeField] private SpriteRenderer gravityAreaRenderer;
    [SerializeField] private SpriteRenderer gravityLineRenderer;

    [SerializeField] private Gradient lineColorGradient;
    [SerializeField] private Gradient areaColorGradient;

    private void Update()
    {
        UpdateColor();
    }

    private void UpdateColor()
    {
        float distanceRatio = ProjectileManager.instance.GetMaxDistanceInActiveProjectile() / GravityZone.GravityZoneRadius;

        gravityAreaRenderer.color = areaColorGradient.Evaluate(distanceRatio);
        gravityLineRenderer.color = lineColorGradient.Evaluate(distanceRatio);
    }
}
