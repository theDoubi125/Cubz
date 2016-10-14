using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CubeBehaviour : ScriptableObject
{
    Vector3 rotationCenter;

    public abstract void OnStart(CubeController cube);
    public abstract void UpdateBehaviour(CubeController cube);
    public abstract void OnGround(CubeController cube);

    public void StartRotation(CubeController cube, Vector3 pos, Vector3 dir)
    {
        cube.StartRotation(pos, dir);
    }

    public void Translate(CubeController cube, Vector3 disp)
    {
        cube.transform.position += disp;
    }

    public void SetRotationSpeed(CubeController cube, float speed)
    {
        cube.SetRotationSpeed(speed);
    }

    public void ChangeBehaviour(CubeController cube, string behaviourName)
    {
        cube.SetBehaviour(behaviourName);
    }
}