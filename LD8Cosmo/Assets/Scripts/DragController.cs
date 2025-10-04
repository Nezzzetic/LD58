using UnityEngine;

public class DragController : MonoBehaviour
{
    private Dragging heldRock = null;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (heldRock == null)
            {
                TryPickUpRock();
            }
            else
            {
                ReleaseHeldRock();
            }
        }

        if (heldRock != null)
        {
            Vector3 mouseWorldPos = GetMouseWorldPositionOnGround();
            heldRock.UpdateHeldPosition(mouseWorldPos + Vector3.up * 0.5f);
        }
    }

    void TryPickUpRock()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Dragging rock = hit.collider.GetComponent<Dragging>();
            if (rock != null && rock.CurrentState != DragState.EnteringHole)
            {
                heldRock = rock;
                heldRock.SetState(DragState.Held);
            }
        }
    }

    public void ReleaseHeldRock()
    {
        heldRock.SetState(DragState.Falling);
        heldRock = null;
    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    Vector3 GetMouseWorldPositionOnGround()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero); // ground at Y = 0

        if (groundPlane.Raycast(ray, out float distance))
        {
            return ray.GetPoint(distance);
        }

        return Vector3.zero;
    }

}

