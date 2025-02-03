using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DraggablePiece : MonoBehaviour
{
    // Cấu hình
    [Header("Drag Settings")]
    [SerializeField] private float smoothFactor = 15f;
    [SerializeField] private LayerMask dragPlaneLayer;
    [SerializeField] private bool usePhysics = true;
    [SerializeField] private bool snapToGrid;
    [SerializeField] private Vector3 gridSize = Vector3.one*6;

    // Thành phần hệ thống
    private Camera mainCamera;
    private Plane dragPlane;
    private Vector3 offset;
    private Rigidbody rb;
    private bool isDragging;
    private float initialDragDistance;

    void Awake()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody>();

        if (rb && usePhysics)
        {
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        }
    }

    void OnMouseDown()
    {
        InitializeDrag();

        //Debug.Log(gameObject.name + "OnMouseDown");
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            UpdateDragPosition();
        }
        //Debug.Log(gameObject.name + "OnMouseDrag");
    }

    void OnMouseUp()
    {
        ReleasePiece();
        //Debug.Log(gameObject.name + "OnMouseUp");
    }

    void InitializeDrag()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, dragPlaneLayer))
        {
            dragPlane = new Plane(Vector3.up, hit.point);
            initialDragDistance = hit.distance;
            offset = transform.position - hit.point;
            isDragging = true;

            if (rb && usePhysics)
            {
                rb.isKinematic = true;
            }
        }
    }

    void UpdateDragPosition()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        float enter;

        if (dragPlane.Raycast(ray, out enter))
        {
            Vector3 targetPosition = ray.GetPoint(enter) + offset;
            //Debug.Log(targetPosition);
            if (snapToGrid)
            {
                targetPosition = SnapPosition(targetPosition);
            }

            ApplyMovement(targetPosition);
        }
    }

    Vector3 SnapPosition(Vector3 position)
    {
        return new Vector3(
            Mathf.Round(position.x / gridSize.x) * gridSize.x,
            0,
            Mathf.Round(position.z / gridSize.z) * gridSize.z
        );
    }

    void ApplyMovement(Vector3 targetPosition)
    {
        if (rb && usePhysics)
        {
            rb.MovePosition(Vector3.Lerp(transform.position, targetPosition, smoothFactor * Time.deltaTime));
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothFactor * Time.deltaTime);
        }
    }

    void ReleasePiece()
    {
        isDragging = false;

        if (rb && usePhysics)
        {
            rb.isKinematic = false;

            // Áp dụng lực dựa trên tốc độ di chuyển chuột
            //Vector3 throwForce = (transform.position - GetPreviousPosition()) / Time.deltaTime;
            //rb.AddForce(throwForce, ForceMode.Impulse);
        }
    }

    Vector3 GetPreviousPosition()
    {
        Vector3 inputPosition = Input.mousePosition;

        // Xử lý cho cảm ứng
#if UNITY_IOS || UNITY_ANDROID
    if (Input.touchCount > 0)
    {
        inputPosition -= (Vector3)Input.GetTouch(0).deltaPosition;
    }
#endif

        Ray ray = mainCamera.ScreenPointToRay(inputPosition);
        float enter;
        return dragPlane.Raycast(ray, out enter) ? ray.GetPoint(enter) + offset : transform.position;
    }

    // Gizmos để hiển thị grid snapping trong Editor
    void OnDrawGizmosSelected()
    {
        if (snapToGrid)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(transform.position, gridSize);
        }
    }
}