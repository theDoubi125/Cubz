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

    public override void OnStart()
    {
        Debug.Log("START ROLLING");
        SetMovementDistance(1);
        currentSpeed = Time.deltaTime * cube.acceleration;
        InitDir();
    }

    public override void OnEnd()
    {
        Debug.Log("END ROLLING");
        currentSpeed = 0;
        climbing = false;
        lastClimb = false;
    }

    public void InitDir()
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
        if (cube.colHandler.IsCellEmpty(Vector3.up))
        {
            if (cube.colHandler.IsCellEmpty(currentDir) && cube.colHandler.IsCellEmpty(currentDir + Vector3.up))
            {
                climbing = false;
                SetMovementDistance(1);
                StartRotation(currentDir + Vector3.down, currentDir);
                SetRotationSpeed(currentSpeed);
            }
            else if (!climbing || cube.colHandler.IsCellEmpty(currentDir))
            {
                climbing = true;
                lastClimb = false;
                if (cube.colHandler.IsCellEmpty(currentDir + Vector3.up))
                    SetMovementDistance(1);
                else
                    SetMovementDistance(1);
                StartRotation(currentDir + Vector3.up, currentDir);
                SetRotationSpeed(currentSpeed);
            }
            else if (cube.colHandler.IsCellEmpty(Vector3.down))
            {
                ChangeBehaviour("Falling");
            }
            else ChangeBehaviour("Standing");

        }
        else ChangeBehaviour("Standing");
    }

    public override void OnGround()
    {
        Debug.Log("OnGround");
        if (!climbing && cube.colHandler.IsCellEmpty(Vector3.down))
            ChangeBehaviour("Falling");
        else if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            InitDir();
        else ChangeBehaviour("Standing");
    }

    public override void OnBack()
    {
        Debug.Log("OnBack");
        if(lastClimb)
        {
            cube.ReverseMovement();
            climbing = true;
            lastClimb = false;
        }
        else
            OnGround();
    }

    public override void UpdateBehaviour()
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
        SetRotationSpeed(currentSpeed);
    }
}