using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CubeBehaviour : ScriptableObject
{
    Vector3 rotationCenter;
    protected CubeController cube;

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
}