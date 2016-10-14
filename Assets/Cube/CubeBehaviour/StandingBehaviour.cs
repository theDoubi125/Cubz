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
    }

    public override void OnGround(CubeController cube)
    {

    }

    public override void UpdateBehaviour(CubeController cube)
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            ChangeBehaviour(cube, "Rolling");
    }
}
