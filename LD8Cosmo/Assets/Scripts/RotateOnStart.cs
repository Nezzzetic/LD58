using UnityEngine;
using UnityEngine.Rendering;

public class RotateOnStart : MonoBehaviour
{
    [Header("Detection Settings")]
    public float radius = 5f;               // Radius to check within
    public LayerMask detectionMask;
    public bool rotateAround;
    public bool randomRotateAround;
    public Transform targetRotateAround;
    public float RotateAroundAngle;

    public bool RandomStartRotation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (rotateAround)
        transform.RotateAround(Vector3.zero, Vector3.up, RotateAroundAngle);
        if (randomRotateAround)
        {
            for (int i = 0; i < 15; i++)
            {
                var rndy = UnityEngine.Random.Range(0, 360f);
                transform.RotateAround(Vector3.zero, Vector3.up, rndy);

                Collider[] hits = Physics.OverlapSphere(transform.position, radius, detectionMask);
                var problem = false;
                foreach (Collider hit in hits)
                {
                    if (hit.attachedRigidbody != null && hit.gameObject != gameObject)
                    {
                        problem = true;
                    }
                }
                if (!problem) { break; }
            }
            
        }
        if (RandomStartRotation)
        {
            var rndx = 0;
            var rndy = UnityEngine.Random.Range(0, 360f);
            var rndz = 0;
            transform.Rotate(Vector3.up * rndx + Vector3.forward * rndy + Vector3.left * rndz, Space.Self);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
