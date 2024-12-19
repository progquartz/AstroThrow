using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SlingState
{
    disabled = 0,
    enabled = 1,
    shooting = 2,
    locked = 3,


}

public class Sling : MonoBehaviour
{

    [SerializeField] private SlingInput slingInput;

    public float ThrowingCooldownTime;

    public SlingState Status { get; private set; }
    private float throwingCooldown = 0;

    private void Awake()
    {
        Status = SlingState.enabled;
    }

    // Update is called once per frame
    void Update()
    {
        if(Status == SlingState.disabled)
        {
            CheckSlingCooldown();
        }
    }

    public void CheckSlingCooldown()
    {
        throwingCooldown += Time.deltaTime;
        if (throwingCooldown > ThrowingCooldownTime)
        {
            throwingCooldown = 0f;
            Status = SlingState.enabled;
        }
    }

    public void SetStatus(SlingState state)
    {
        Status = state;
    }
}
