using UnityEngine;

public class Invising : MonoBehaviour
{
    public float Distance;
    public GameObject hiddenObject;
    public bool hidden;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HideChange(bool state)
    {
        if (state!=hidden) { 
            hidden = state;
        hiddenObject.SetActive(hidden);
        }
    }
}
