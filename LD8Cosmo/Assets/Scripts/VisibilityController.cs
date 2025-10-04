using System.Collections.Generic;
using UnityEngine;

public class VisibilityController : MonoBehaviour
{
    public List<Invising> Invisings;

    void Update()
    {
        
            Vector3 mouseWorldPos = GetMouseWorldPositionOnGround();
        if (mouseWorldPos != Vector3.zero)
        {
            foreach (var invising in Invisings) {
                var distance = Vector3.Distance(invising.transform.position, mouseWorldPos);
                Debug.Log(distance.ToString());
                invising.HideChange(distance < invising.Distance);
            }

        }
        
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
