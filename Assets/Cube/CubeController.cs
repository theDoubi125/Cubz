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
    public float currentSpeed, currentPos, movementDistance;
    private Vector3 currentDir;
    public float gravity;

    private Vector3 relativeRotCenter;
    private Vector3 translationVelocity;

    public CollisionHandler colHandler;
    public CubeBehaviour behaviour;

    public Vector3 right, forward;

    public Dictionary<string, CubeBehaviour> behaviours;

	void Start ()
    {
        colHandler = GetComponent<CollisionHandler>();
        behaviours = new Dictionary<string, CubeBehaviour>();
        behaviours.Add("Standing", ScriptableObject.CreateInstance<StandingBehaviour>());
        behaviours.Add("Rolling", ScriptableObject.CreateInstance<RollingBehaviour>());
        behaviours.Add("Falling", ScriptableObject.CreateInstance<FallingBehaviour>());
        SetBehaviour("Standing");
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
        behaviour.SetCube(this);
        behaviour.OnStart();
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

    public void SetTranslationVelocity(Vector3 velocity)
    {
        translationVelocity = velocity;
    }

    public void SetMovementDistance(float distance)
    {
        this.movementDistance = distance;
    }

    public float GetInputWork()
    {
        return Vector3.Dot(currentDir, inputVector);
    }

    public void ReverseMovement()
    {
        currentPos = movementDistance;
    }
	
	private void UpdateMovement ()
    {
        forward = Vector3.zero;
        right = Vector3.zero;
        ComputeInputVectors();

        behaviour.UpdateBehaviour();

        // Update
        float deltaTime = Time.deltaTime;

        bool isOnGround = false;
        bool isBack = false;
        if (currentSpeed != 0)
        {
            if (currentPos + deltaTime * currentSpeed >= movementDistance)
            {
                deltaTime = (movementDistance - currentPos) / currentSpeed;
                isOnGround = true;
                currentPos = 0;
            }
            if (currentPos + deltaTime * currentSpeed < 0)
            {
                deltaTime = -currentPos / currentSpeed;
                isBack = true;
                currentPos = 0;
            }
        }
        transform.position += translationVelocity * deltaTime * currentSpeed;
        if (currentDir != Vector3.zero)
        {
            transform.RotateAround(transform.position
                + transform.right * relativeRotCenter.x/2
                + transform.up * relativeRotCenter.y/2
                + transform.forward * relativeRotCenter.z/2
                , Vector3.Cross(Vector3.up, currentDir)
                , 90 * deltaTime * currentSpeed
            );
        }

        if (isOnGround)
        {
            behaviour.OnGround();
        }
        else if(isBack)
        {
            behaviour.OnBack();
        }
        else
            currentPos += deltaTime * currentSpeed;
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

    public Vector3 GetMainInputDirection()
    {
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > Mathf.Abs(Input.GetAxis("Vertical")))
            return(right * Input.GetAxis("Horizontal")).normalized;
        else
            return (forward * Input.GetAxis("Vertical")).normalized;
    }

    public Vector3 inputVector { get { return right * Input.GetAxis("Horizontal") + forward * Input.GetAxis("Vertical"); } }
}
