using UnityEngine;

public class RotateOnStart : MonoBehaviour
{
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
            var rndy = UnityEngine.Random.Range(0, 360f);
            transform.RotateAround(Vector3.zero, Vector3.up, rndy);
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
