using Unity.VisualScripting;
using UnityEngine;

using Touch = UnityEngine.Touch;

public class SlingInput : MonoBehaviour
{
    private Sling sling;
    private Collider2D touchCollider2D;
    private Vector2 touchStartPosition;
    [SerializeField] private Transform slingStartPosition;


    //private bool isTouchStarted = false;
    private float maxPowerDrag = 2.5f;
    private float projectileForceMultiplier = 5f;

    [SerializeField] private TrajectoryDrawer trajectoryDrawer;
    [SerializeField] private GameObject throwingObject;


    private void Awake()
    {
        touchCollider2D = GetComponent<Collider2D>();
        sling = GetComponent<Sling>();
    }

    void Update()
    {
        if(sling.Status == SlingState.enabled || sling.Status == SlingState.shooting)
        {
            bool isShootingFinished = false;
            if (Application.isMobilePlatform)
            {
                HandleTouchInput();
            }
            else
            {
                HandleMouseInput();
            }
        }
    }

    private void HandleTouchInput()
    {
        /*
        if (Input.touchCount <= 0) return;

        // 터치 입력 시,
        Touch touch = Input.GetTouch(0);
        Vector2 worldTouchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

        switch (touch.phase)
        {
            case TouchPhase.Began:
                // 터치 시작 지점 확인
                if (touchCollider2D == Physics2D.OverlapPoint(worldTouchPos))
                {
                    touchStartPosition = slingStartPosition.position;
                    isTouchStarted = true;
                    throwingObject = ProjectileManager.instance.GetNewProjectile(touchStartPosition);
                    throwingObject.transform.position = slingStartPosition.position;
                    throwingObject.transform.rotation = Quaternion.identity;
                }
                break;

            case TouchPhase.Stationary:
            case TouchPhase.Moved:
                // 터치 당기기
                if (isTouchStarted)
                {
                    Vector2 limitDistance = LimitDragDistance(worldTouchPos);
                    throwingObject.transform.position = limitDistance;
                    trajectoryDrawer.DrawTrajectory(throwingObject.transform.position, CalculateSpeedVector(touchStartPosition, limitDistance, throwingObject.GetComponent<Rigidbody2D>()), throwingObject.GetComponent<Rigidbody2D>());
                }
                break;

            case TouchPhase.Ended:
            case TouchPhase.Canceled:
                if (isTouchStarted)
                {
                    // 터치 놓기.
                    isTouchStarted = false;
                    Vector2 limitDistance = LimitDragDistance(worldTouchPos);
                    Debug.Log((limitDistance - touchStartPosition).magnitude);
                    throwingObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                    throwingObject.GetComponent<Rigidbody2D>().AddForce((touchStartPosition - limitDistance) * projectileForceMultiplier, ForceMode2D.Impulse);
                }
                break;
        }
        */

    }

    private void HandleMouseInput()
    {
        // 마우스 입력 시,
        Vector2 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            // 마우스 클릭 시작 지점 확인
            if (touchCollider2D == Physics2D.OverlapPoint(worldMousePos))
            {
                touchStartPosition = slingStartPosition.position;
                sling.SetStatus(SlingState.shooting);
                throwingObject = ProjectileManager.instance.GetNewProjectile(touchStartPosition);
            }
        }

        else if (Input.GetMouseButton(0))
        {
            // 마우스 드래그 중
            if (sling.Status == SlingState.shooting && throwingObject != null)
            {
                Vector2 limitDistance = LimitDragDistance(worldMousePos);
                throwingObject.transform.position = limitDistance;

                trajectoryDrawer.DrawTrajectory(throwingObject.transform.position, CalculateSpeedVector(touchStartPosition, limitDistance, throwingObject.GetComponent<Rigidbody2D>()), throwingObject.GetComponent<Rigidbody2D>());
            }
        }

        else if (Input.GetMouseButtonUp(0))
        {
            // 마우스 클릭 해제
            if (sling.Status == SlingState.shooting && throwingObject != null)
            {
                sling.SetStatus(SlingState.disabled);
                Vector2 limitDistance = LimitDragDistance(worldMousePos);
                Debug.Log("Power is :" + (limitDistance - touchStartPosition).magnitude);
                throwingObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                throwingObject.GetComponent<Rigidbody2D>().AddForce((touchStartPosition - limitDistance) * projectileForceMultiplier, ForceMode2D.Impulse);
                trajectoryDrawer.EraseTrajectory();

                throwingObject = null;

            }
        }
    }

    private Vector3 CalculateSpeedVector(Vector2 touchStartPosition, Vector2 worldMousePos, Rigidbody2D throwRB)
    {
        Vector2 impulseForce = (touchStartPosition - worldMousePos) * projectileForceMultiplier;
        Vector2 velocityChange = impulseForce / throwRB.mass;
        return velocityChange;
    }

    private Vector2 LimitDragDistance(Vector2 dragPosition)
    {
        if ((dragPosition - touchStartPosition).magnitude > maxPowerDrag)
        {
            return touchStartPosition + (dragPosition - touchStartPosition).normalized * maxPowerDrag;
        }
        return dragPosition;
    }

}
