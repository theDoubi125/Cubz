using UnityEngine;
using System.Collections.Generic;

public abstract class MovingBlock : MonoBehaviour
{
    public float speed;
    private Vector3 direction;
    private float duration;

    public Vector3 currentDirection { get { return direction; } }

    private List<CubeController> waitForAttach = new List<CubeController>();
    private List<CubeController> waitForDetach = new List<CubeController>();
    private List<CubeController> attachedCubes = new List<CubeController>();


    void Start()
    {
        
    }

    void Update()
    {
        UpdateMovement();
        if (duration > 0)
        {
            float deltaTime = Mathf.Min(Time.deltaTime, duration);
            transform.position += deltaTime * speed * direction;
            duration -= deltaTime;
            if (duration == 0)
                OnMovementFinished();
        }
    }

    public void StartMovement(Vector3 direction)
    {
        this.direction = direction.normalized;
        this.duration = direction.magnitude / speed;
    }


    public abstract void UpdateMovement();
    public abstract void OnMovementFinished();

    void OnTriggerEnter(Collider collider)
    {
        CubeController controller = collider.GetComponent<CubeController>();
        if (controller)
        {
            controller.PushedBy(this);
        }
    }

    void OnTriggerStay(Collider collider)
    {
        CubeController controller = collider.GetComponent<CubeController>();
        if (controller)
        {
            controller.PushedBy(this);
        }
    }
}
