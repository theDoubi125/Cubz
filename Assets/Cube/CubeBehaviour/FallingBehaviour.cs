using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBehaviour : CubeBehaviour
{
    public override void OnGround(CubeController cube)
    {
        SetTranslation(cube, Vector3.zero);
        ChangeBehaviour(cube, "Standing");
    }

    public override void OnStart(CubeController cube)
    {
        SetTranslation(cube, Vector3.down);
        StartRotation(cube, Vector3.zero, Vector3.zero);
        SetRotationSpeed(cube, 1);
    }

    public override void UpdateBehaviour(CubeController cube)
    {

    }
}
