using UnityEngine;

public class Moving : MonoBehaviour
{
    [Header("Spin Settings")]
    public Transform targetPoint; // The point to rotate around
    public float rotationSpeed = 50f; // Degrees per second
    public Vector3 rotationAxis = Vector3.up; // Axis to rotate around

    private Dragging Dragging;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Dragging = GetComponent<Dragging>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Dragging != null && Dragging.CurrentState!=DragState.Held) {
            Move();
        }
    }

    private void Move()
    {
        transform.RotateAround(
            targetPoint.position,    // Center of rotation
            rotationAxis,            // Axis (e.g. Vector3.up)
            rotationSpeed * Time.deltaTime  // Angle per frame
        );
    }
}
