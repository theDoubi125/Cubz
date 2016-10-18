using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CubeBehaviour : ScriptableObject
{
    Vector3 rotationCenter;
    protected CubeController cube;
    List<MovingBlock> attachedBlocks = new List<MovingBlock>();

    public abstract void OnStart();
    public abstract void UpdateBehaviour();
    public abstract void OnGround();
    public abstract void OnEnd();
    public abstract void OnBack();

    public void StartRotation(Vector3 pos, Vector3 dir)
    {
        cube.StartRotation(pos, dir);
    }

    public void SetTranslation(Vector3 disp)
    {
        Debug.Log("Set Translation " + disp);
        cube.SetTranslationVelocity(disp);
    }

    public void SetMovementDistance(float distance)
    {
        cube.SetMovementDistance(distance);
    }

    public void Translate(Vector3 disp)
    {
        cube.transform.position += disp;
    }

    public void SetRotationSpeed(float speed)
    {
        cube.SetRotationSpeed(speed);
    }

    public void ChangeBehaviour(string behaviourName)
    {
        OnEnd();
        cube.SetBehaviour(behaviourName);
    }

    public void SetCube(CubeController cube)
    {
        this.cube = cube;
    }

    public void AttachTo(Vector3 pos)
    {
        foreach (Collider collider in cube.colHandler.GetCollidingEntities(pos))
        {
            MovingBlock block = collider.GetComponent<MovingBlock>();
            if (block)
            {
                block.AttachCube(cube.transform);
                attachedBlocks.Add(block);
            }
        }
    }

    public void DetachAll()
    {
        foreach(MovingBlock attachedBlock in attachedBlocks)
        {
            attachedBlock.DetachCube(cube.transform);
        }
        attachedBlocks.Clear();
    }

    public void DetachTo(Vector3 pos)
    {
        foreach (Collider collider in cube.colHandler.GetCollidingEntities(pos))
        {
            MovingBlock block = collider.GetComponent<MovingBlock>();
            if (block)
            {
                block.DetachCube(cube.transform);
            }
        }
    }
}