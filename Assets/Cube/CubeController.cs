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

    private CollisionHandler colHandler;

	void Start ()
    {
        colHandler = GetComponent<CollisionHandler>();
	}

    void Update()
    {
        UpdateCollisions();
        UpdateMovement();
    }

    private void UpdateCollisions()
    {
        
    }
	
	private void UpdateMovement ()
    {
        Vector3 forward = Vector3.zero, right = Vector3.zero;
        ComputeInputVectors(ref right, ref forward);
        //print(Vector3.Project(camTransform.forward, Vector3.right).magnitude + " " + Vector3.Project(camTransform.right, Vector3.right).magnitude);
        
        if (currentDir == Vector3.zero)
        {
            ComputeDirection(right, forward);
            ComputeRotationCenter();
        }
        float deltaTime = Time.deltaTime;
        bool movementFinished = false;
        UpdateSpeed(right, forward);
        if(currentPos + deltaTime * currentSpeed >= 1)
        {
            movementFinished = true;
            deltaTime = (1 - currentPos)/currentSpeed;
        }
        if (currentPos + deltaTime * currentSpeed < 0)
        {
            movementFinished = true;
            deltaTime =  -currentPos/currentSpeed;
        }
        if (currentDir != Vector3.zero)
        {
            if(currentDir == Vector3.down)
            {

            }
            else
            {
                transform.RotateAround(transform.position
                    + transform.right * relativeRotCenter.x/2
                    + transform.up * relativeRotCenter.y/2
                    + transform.forward * relativeRotCenter.z/2
                    , Vector3.Cross(Vector3.up, currentDir)
                    , 90 * deltaTime * currentSpeed
                );
            }
            currentPos += deltaTime * currentSpeed;
            //transform.position += currentSpeed * deltaTime * currentDir;
            if (movementFinished)
            {
                ComputeDirection(right, forward);
                if (currentDir == Vector3.zero)
                    currentSpeed = 0;
                else
                    currentSpeed = Mathf.Abs(currentSpeed);
                currentPos = 0;
                ComputeRotationCenter();
            }
        }
    }

    private void ComputeInputVectors(ref Vector3 right, ref Vector3 forward)
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

    private void ComputeDirection(Vector3 right, Vector3 forward)
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
    }

    private void UpdateSpeed(Vector3 right, Vector3 forward)
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
}
