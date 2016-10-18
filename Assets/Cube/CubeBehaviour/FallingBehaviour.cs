using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBehaviour : CubeBehaviour
{
    private float currentSpeed;

    public override void OnGround()
    {
        SetMovementDistance(1);
        if (!cube.colHandler.IsCellEmpty(Vector3.down))
            ChangeBehaviour("Standing");
    }

    public override void OnBack()
    {
        OnGround();
    }

    public override void OnStart()
    {
        Debug.Log("START FALLING");
        SetTranslation(Vector3.down);
        StartRotation(Vector3.zero, Vector3.zero);
        currentSpeed = 0;
    }

    public override void OnEnd()
    {
        Debug.Log("END FALLING");
        SetTranslation(Vector3.zero);
    }

    public override void UpdateBehaviour()
    {
        currentSpeed += cube.gravity * Time.deltaTime;
        SetRotationSpeed(currentSpeed);

    }
}
