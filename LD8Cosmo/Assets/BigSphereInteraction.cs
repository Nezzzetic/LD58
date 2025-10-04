using System;
using UnityEngine;

public class BigSphereInteraction : MonoBehaviour
{
    public VisibilityController VisibilityController;
    
    private float[] ScaleList;
    public float scaleChangeMaxSpeed;
    private float currentScale;
    private float currentTargetScale;
    private bool scaling;



    private void Start()
    {
        ScaleList = new float[]{ 1.5f,3 };
        scaling = false;
    }

    private void Update()
    {
        ScaleUpdate();
    }
    private void OnTriggerEnter(Collider other)
    {
        var consume = other.GetComponent<Consuming>();
        if (consume != null)
        {
            ScaleChange(ScaleList[consume.ConsumeClass]);
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

    void ScaleChange(float addScale)
    {
        currentTargetScale += addScale;
        scaling = true;
    }

        void ScaleUpdate()
        {
        if (scaling) { 
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(currentTargetScale, 0.1f, currentTargetScale), Time.deltaTime * scaleChangeMaxSpeed);
            if (currentTargetScale - transform.localScale.x < 0.1f) scaling = false;
            }
        }
    }