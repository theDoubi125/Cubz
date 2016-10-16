using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBehaviour : CubeBehaviour
{
    private float currentSpeed;

    public override void OnGround(CubeController cube)
    {
        SetMovementDistance(cube, 1);
        if (!cube.colHandler.IsCellEmpty(Vector3.down))
            ChangeBehaviour(cube, "Standing");
    }

    public override void OnBack(CubeController cube)
    {
        OnGround(cube);
    }

    public override void OnStart(CubeController cube)
    {
        Debug.Log("START FALLING");
        SetTranslation(cube, Vector3.down);
        StartRotation(cube, Vector3.zero, Vector3.zero);
        currentSpeed = 0;
    }

    public override void OnEnd(CubeController cube)
    {
        Debug.Log("END FALLING");
        SetTranslation(cube, Vector3.zero);
    }

    public override void UpdateBehaviour(CubeController cube)
    {
        currentSpeed += cube.gravity * Time.deltaTime;
        SetRotationSpeed(cube, currentSpeed);

    }
}
