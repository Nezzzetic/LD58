using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Dragging : MonoBehaviour
{
    public DragState CurrentState { get; private set; } = DragState.Idle;
    private Rigidbody rb;
    public Collider Mycollider;

    public event Action<DragState> OnStateChanged;

    private Vector3 targetPosition;
    public float followSpeed = 20f; // how fast it snaps into position

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        var rndx = 0;
        var rndy = UnityEngine.Random.Range(0, 360f);
        var rndz = 0;
        transform.Rotate(Vector3.up * rndx + Vector3.forward * rndy + Vector3.left * rndz, Space.Self);
    }

    void Update()
    {
        if (CurrentState == DragState.Held)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * followSpeed);
        }
        else {
            transform.Rotate(Vector3.up * 30 * Time.deltaTime, Space.Self);
        }
    }

    public void SetState(DragState newState)
    {
        if (CurrentState == newState) return;

        CurrentState = newState;
        OnStateChanged?.Invoke(CurrentState); // notify listeners

        switch (CurrentState)
        {
            case DragState.Held:
                rb.useGravity = false;
                rb.linearVelocity = Vector3.zero;
                //Mycollider.enabled = false;
                break;

            case DragState.Falling:
            case DragState.Idle:
                rb.useGravity = true;
                //Mycollider.enabled = true;
                break;
        }
    }

    public void UpdateHeldPosition(Vector3 target)
    {
        if (CurrentState == DragState.Held)
        {
            targetPosition = target + Vector3.right * 0.25f + Vector3.back * 0.25f;
        }
    }
}
