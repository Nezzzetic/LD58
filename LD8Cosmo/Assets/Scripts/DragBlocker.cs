using UnityEngine;

public class DragBlocker : MonoBehaviour
{

    public DragController DragController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        var consume = other.GetComponent<Dragging>();
        if (consume != null)
        {
            DragController.ReleaseHeldRock();
        }
    }
}
