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
    }

    void Update()
    {
        if (CurrentState == DragState.Held)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * followSpeed);
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
