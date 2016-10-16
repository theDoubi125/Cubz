using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StandingBehaviour : CubeBehaviour
{
    public override void OnStart(CubeController cube)
    {
        Debug.Log("START STANDING");
        SetRotationSpeed(cube, 0);
        SetMovementDistance(cube, 1);
    }

    public override void OnEnd(CubeController cube)
    {
        Debug.Log("END STANDING");

    }

    public override void OnGround(CubeController cube)
    {

    }

    public override void OnBack(CubeController cube)
    {
        OnGround(cube);
    }

    public override void UpdateBehaviour(CubeController cube)
    {
        if (cube.colHandler.IsCellEmpty(Vector3.down))
            ChangeBehaviour(cube, "Falling");
        else if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            ChangeBehaviour(cube, "Rolling");
    }
}
