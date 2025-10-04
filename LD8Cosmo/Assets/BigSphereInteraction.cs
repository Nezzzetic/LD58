using UnityEngine;

public class BigSphereInteraction : MonoBehaviour
{
    public VisibilityController VisibilityController;
    private float[] ScaleList;

    private void Start()
    {
        ScaleList = new float[]{ 1.5f,3 };
    }
    private void OnTriggerEnter(Collider other)
    {
        var consume = other.GetComponent<Consuming>();
        if (consume != null)
        {
            transform.localScale += new Vector3(ScaleList[consume.ConsumeClass], ScaleList[consume.ConsumeClass], ScaleList[consume.ConsumeClass]);
            var invischeck = false;
            var consumeInvis = consume.GetComponent<Invising>();
            if (consumeInvis != null) { 
            foreach (var item in VisibilityController.Invisings)
            {
                if (item == consumeInvis) invischeck = true;
            }
            if (invischeck) VisibilityController.Invisings.Remove(other.GetComponent<Invising>());
            }
            Destroy(other.gameObject);
        }
    }
}