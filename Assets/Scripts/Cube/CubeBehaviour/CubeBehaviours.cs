using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CubeBehaviour : ScriptableObject
{
    private Vector3 m_rotationCenter;
    protected CubeController m_cube;
    private List<MovingBlock> m_attachedBlocks = new List<MovingBlock>();

    public abstract void OnStart();
    public abstract void UpdateBehaviour();
    public abstract void OnGround();
    public abstract void OnEnd();
    public abstract void OnBack();

    public void StartRotation(Vector3 pos, Vector3 dir)
    {
        m_cube.StartRotation(pos, dir);
    }

    public void SetTranslation(Vector3 disp)
    {
        m_cube.SetTranslationVelocity(disp);
    }

    public void SetMovementDistance(float distance)
    {
        m_cube.SetMovementDistance(distance);
    }

    public void Translate(Vector3 disp)
    {
        m_cube.transform.position += disp;
    }

    public void SetRotationSpeed(float speed)
    {
        m_cube.SetRotationSpeed(speed);
    }

    public void ChangeBehaviour(string behaviourName)
    {
        OnEnd();
        m_cube.SetBehaviour(behaviourName);
    }

    public void SetCube(CubeController cube)
    {
        this.m_cube = cube;
    }

    public MovingBlock GetMovingBlockAt(Vector3 pos)
    {
        foreach (Collider collider in m_cube.colHandler.GetCollidingEntities(pos))
        {
            MovingBlock block = collider.GetComponent<MovingBlock>();
            if (block)
            {
                return block;
            }
        }
		return null;
    }
}