using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BigSphereInteraction : MonoBehaviour
{
    public VisibilityController VisibilityController;
    
    private float[] ScaleList;
    public float scaleChangeMaxSpeed;
    private float currentScale;
    private float currentTargetScale;
    private bool scaling;
    private bool scalingEnd;
    public float scaleChangeEndSpeed;
    public float scaleToEnd;
    public float scaleEnd;
    public float TimerToEndLvl;
    public int scene;
    public AudioSource AudioSource;
    public AudioClip[] audioClips;
    float numTimer;
    public GameObject puff;

    private void Awake()
    {
        AudioSource=GetComponent<AudioSource>();
    }

    private void Start()
    {
        ScaleList = new float[]{ 2,4,50 };
        scaling = false;
        currentTargetScale = transform.localScale.x;
    }

    private void Update()
    {
        ScaleUpdate();
        ScaleUpdateEnd();
        if (numTimer >= 0)
        {
            numTimer -= Time.deltaTime; if (numTimer < 0) numTimer = -1;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        var consume = other.GetComponent<Consuming>();
        if (consume != null)
        {
            ScaleChange(ScaleList[consume.ConsumeClass]/ (0.8f+currentTargetScale * 0.1f));
            if (numTimer<0) { 
            var rndClip= UnityEngine.Random.Range(0, audioClips.Length-1);
            AudioSource.clip = audioClips[rndClip];
            AudioSource.Play();
                numTimer = 0.1f;
            }
            var invischeck = false;
            var consumeInvis = consume.GetComponent<Invising>();
            if (consumeInvis != null) { 
            foreach (var item in VisibilityController.Invisings)
            {
                if (item == consumeInvis) invischeck = true;
            }
            if (invischeck) VisibilityController.Invisings.Remove(other.GetComponent<Invising>());
            }
            var pufflocal=Instantiate(puff,other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(pufflocal,1);
        }
    }

    void ScaleChange(float addScale)
    {
        if (scalingEnd) return;
        currentTargetScale += addScale;
        if (currentTargetScale> scaleToEnd)
        {
            scaling = false;
            scalingEnd = true;
        }else
        {
            scaling = true;
        }
    }

        void ScaleUpdate()
        {
        if (scaling) { 
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(currentTargetScale, 0.1f, currentTargetScale), Time.deltaTime * scaleChangeMaxSpeed);
            if (currentTargetScale - transform.localScale.x < 0.05f)
            {
                scaling = false;
                currentTargetScale= transform.localScale.x;
            }
            }
        }

    void ScaleUpdateEnd()
    {

        if (scalingEnd)
        {
            transform.localScale += new Vector3(1, 0, 1) * scaleChangeEndSpeed * Time.deltaTime;
            TimerToEndLvl-= Time.deltaTime;

            if (TimerToEndLvl<0)
            {
                NextLvl();
            }
        }
    }

    void NextLvl()
    {
        SceneManager.LoadScene(scene + 1);
    }

    public void Feed()
    {
        ScaleChange(50);
        scalingEnd=true;
    }
}