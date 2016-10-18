using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StandingBehaviour : CubeBehaviour
{
    public override void OnStart()
    {
        Debug.Log("START STANDING");
        SetRotationSpeed(0);
        SetMovementDistance(1);
    }

    public override void OnEnd()
    {
        Debug.Log("END STANDING");

    }

    public override void OnGround()
    {

    }

    public override void OnBack()
    {
        OnGround();
    }

    public override void UpdateBehaviour()
    {
        if (cube.colHandler.IsCellEmpty(Vector3.down))
            ChangeBehaviour("Falling");
        else if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            ChangeBehaviour("Rolling");
    }
}
