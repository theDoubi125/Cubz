using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Up, Down, Left, Right, Forward, Back, None
}

public class CubeController : MonoBehaviour
{
    public float cellSize;

    private Direction movingDir = Direction.None;
    private float movingTime;

    private Vector3 rotCenter;
    public float acceleration, deceleration, maxSpeed;
    public float currentSpeed, currentPos;
    private Vector3 currentDir;
    public float gravity;

    private Vector3 relativeRotCenter;

    public CollisionHandler colHandler;
    public CubeBehaviour behaviour;

    public Vector3 right, forward;

    public Dictionary<string, CubeBehaviour> behaviours; 

	void Start ()
    {
        colHandler = GetComponent<CollisionHandler>();
        behaviour = ScriptableObject.CreateInstance<StandingBehaviour>();
        behaviours = new Dictionary<string, CubeBehaviour>();
        behaviours.Add("Standing", ScriptableObject.CreateInstance<StandingBehaviour>());
        behaviours.Add("Rolling", ScriptableObject.CreateInstance<RollingBehaviour>());
    }

    void Update()
    {
        UpdateMovement();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position
                + transform.right * relativeRotCenter.x / 2
                + transform.up * relativeRotCenter.y / 2
                + transform.forward * relativeRotCenter.z / 2, transform.position);
    }

    private CubeBehaviour GetBehaviour(string name)
    {
        return behaviours[name];
    }

    public void SetBehaviour(string behaviourName)
    {
        this.behaviour = GetBehaviour(behaviourName);
        behaviour.OnStart(this);
    }

    public void StartRotation(Vector3 center, Vector3 dir)
    {
        relativeRotCenter.x = Vector3.Dot(transform.right, center);
        relativeRotCenter.y = Vector3.Dot(transform.up, center);
        relativeRotCenter.z = Vector3.Dot(transform.forward, center);
        currentDir = dir;
    }

    public void SetRotationSpeed(float speed)
    {
        currentSpeed = speed;
    }

    private void UpdateCollisions()
    {
        
    }
	
	private void UpdateMovement ()
    {
        forward = Vector3.zero;
        right = Vector3.zero;
        ComputeInputVectors();
        //print(Vector3.Project(camTransform.forward, Vector3.right).magnitude + " " + Vector3.Project(camTransform.right, Vector3.right).magnitude);
        
        // Brain
        /*if (currentDir == Vector3.zero)
        {
            ComputeDirection();
            ComputeRotationCenter();
        }*/
        //UpdateSpeed();

        behaviour.UpdateBehaviour(this);

        // Update
        float deltaTime = Time.deltaTime;
        //bool movementFinished = false;

        bool isOnGround = false;
        if (currentSpeed != 0)
        {
            if (currentPos + deltaTime * currentSpeed >= 1)
            {
                deltaTime = (1 - currentPos) / currentSpeed;
                isOnGround = true;
                currentPos = 0;
            }
            if (currentPos + deltaTime * currentSpeed < 0)
            {
                deltaTime = -currentPos / currentSpeed;
                isOnGround = true;
                currentPos = 0;
            }
        }
        if (currentDir != Vector3.zero)
        {
            transform.RotateAround(transform.position
                + transform.right * relativeRotCenter.x/2
                + transform.up * relativeRotCenter.y/2
                + transform.forward * relativeRotCenter.z/2
                , Vector3.Cross(Vector3.up, currentDir)
                , 90 * deltaTime * currentSpeed
            );
            if(!isOnGround)
                currentPos += deltaTime * currentSpeed;
        }
        if(isOnGround)
            behaviour.OnGround(this);
    }

    private void ComputeInputVectors()
    {
        Transform camTransform = Camera.main.transform;
        if (Vector3.Project(camTransform.forward, Vector3.right).sqrMagnitude > Vector3.Project(camTransform.right, Vector3.right).sqrMagnitude)
        {
            forward = Vector3.Project(camTransform.forward, Vector3.right).normalized;
            right = Vector3.Project(camTransform.right, Vector3.forward).normalized;
        }
        else
        {
            right = Vector3.Project(camTransform.right, Vector3.right).normalized;
            forward = Vector3.Project(camTransform.forward, Vector3.forward).normalized;
        }
    }

    /*private void ComputeDirection()
    {
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > Mathf.Abs(Input.GetAxis("Vertical")))
            currentDir = (right * Input.GetAxis("Horizontal")).normalized;
        else
            currentDir = (forward * Input.GetAxis("Vertical")).normalized;
        if (!colHandler.IsCellEmpty(currentDir) && !colHandler.IsCellEmpty(0, 1, 0))
            currentDir = Vector3.zero;
    }

    private void ComputeRotationCenter()
    {
        if(colHandler.IsCellEmpty(currentDir))
        {
            relativeRotCenter.x = Vector3.Dot(currentDir + Vector3.down, transform.right);
            relativeRotCenter.y = Vector3.Dot(currentDir + Vector3.down, transform.up);
            relativeRotCenter.z = Vector3.Dot(currentDir + Vector3.down, transform.forward);
        }
        else
        {
            relativeRotCenter.x = Vector3.Dot(currentDir + Vector3.up, transform.right);
            relativeRotCenter.y = Vector3.Dot(currentDir + Vector3.up, transform.up);
            relativeRotCenter.z = Vector3.Dot(currentDir + Vector3.up, transform.forward);
        }
    }*/

    private void UpdateSpeed()
    {
        Vector3 inputVector = right * Input.GetAxis("Horizontal") + forward * Input.GetAxis("Vertical");
        if (currentDir != Vector3.zero)
        {
            if (currentPos < 0.5)
                currentSpeed -= gravity * Time.deltaTime;
            else
                currentSpeed += gravity * Time.deltaTime;
        }
        currentSpeed += Vector3.Dot(currentDir, inputVector) * Time.deltaTime * acceleration;
        if (currentSpeed > maxSpeed)
            currentSpeed = maxSpeed;

    }

    public Vector3 inputVector { get { return right * Input.GetAxis("Horizontal") + forward * Input.GetAxis("Vertical"); } }
}
