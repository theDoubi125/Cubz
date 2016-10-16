using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RollingBehaviour : CubeBehaviour
{
    private bool climbing, lastClimb;
    private Vector3 currentDir;
    private float currentSpeed;

    public override void OnStart(CubeController cube)
    {
        Debug.Log("START ROLLING");
        SetMovementDistance(cube, 1);
        currentSpeed = Time.deltaTime * cube.acceleration;
        InitDir(cube);
    }

    public override void OnEnd(CubeController cube)
    {
        Debug.Log("END ROLLING");
        currentSpeed = 0;
        climbing = false;
        lastClimb = false;
    }

    public void InitDir(CubeController cube)
    {
        currentSpeed = Mathf.Abs(currentSpeed);
        if(!climbing)
        {
            if (Mathf.Abs(Input.GetAxis("Horizontal")) > Mathf.Abs(Input.GetAxis("Vertical")))
                currentDir = (cube.right * Input.GetAxis("Horizontal")).normalized;
            else
                currentDir = (cube.forward * Input.GetAxis("Vertical")).normalized;
        }
        lastClimb = climbing;
        if (cube.colHandler.IsCellEmpty(currentDir))
        {
            climbing = false;
            SetMovementDistance(cube, 1);
            StartRotation(cube, currentDir + Vector3.down, currentDir);
            SetRotationSpeed(cube, currentSpeed);
        }
        else if (!climbing)
        {
            climbing = true;
            if (cube.colHandler.IsCellEmpty(currentDir + Vector3.up))
                SetMovementDistance(cube, 1);
            else
                SetMovementDistance(cube, 1);
            StartRotation(cube, currentDir + Vector3.up, currentDir);
            SetRotationSpeed(cube, currentSpeed);
        }
        else if (cube.colHandler.IsCellEmpty(Vector3.down))
        {
            ChangeBehaviour(cube, "Falling");
        }
        else ChangeBehaviour(cube, "Standing");
    }

    public override void OnGround(CubeController cube)
    {
        Debug.Log("OnGround");
        if (!climbing && cube.colHandler.IsCellEmpty(Vector3.down))
            ChangeBehaviour(cube, "Falling");
        else if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            InitDir(cube);
        else ChangeBehaviour(cube, "Standing");
    }

    public override void OnBack(CubeController cube)
    {
        Debug.Log("OnBack");
        if(lastClimb)
        {
            cube.ReverseMovement();
            climbing = true;
            lastClimb = false;
        }
        else
            OnGround(cube);
    }

    public override void UpdateBehaviour(CubeController cube)
    {
        currentSpeed += Time.deltaTime * cube.acceleration * cube.GetInputWork();
        if (climbing)
            currentSpeed -= cube.gravity * Time.deltaTime;
        else
        {
            if (cube.currentPos < 0.5f)
                currentSpeed -= cube.gravity * Time.deltaTime;
            else currentSpeed += cube.gravity * Time.deltaTime;
        }
        if (currentSpeed > cube.maxSpeed)
            currentSpeed = cube.maxSpeed;
        SetRotationSpeed(cube, currentSpeed);
    }
}