using UnityEngine;

public class Teleporting : MonoBehaviour
{


    private Dragging Dragging;
    public float TimerMax;
    private float Timer;
    public Vector2 x;
    public Vector2 z;

    void Awake()
    {
        Dragging = GetComponent<Dragging>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Timer = TimerMax;
    }


    void Update()
    {
        if (Dragging != null && Dragging.CurrentState != DragState.Held)
        {
            Move();
        } else Timer = TimerMax;
    }

    private void Move()
    {

        Timer -= Time.deltaTime;
        if (Timer < 0)
        {
            Timer -= Time.deltaTime;
            var rndx = Random.Range(x.x, x.y);
            var rndz = Random.Range(z.x, z.y);
            transform.position=new Vector3(rndx, 1, rndz);
            Timer = TimerMax;
        }
    }
}
