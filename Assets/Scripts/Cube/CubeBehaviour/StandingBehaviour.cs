using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StandingBehaviour : CubeBehaviour
{
    public override void OnStart()
    {
        SetRotationSpeed(0);
        SetMovementDistance(1);
        //AttachTo(Vector3.down);
    }

    public override void OnEnd()
    {
        //DetachTo(Vector3.down);
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
        if (m_cube.colHandler.IsCellEmpty(Vector3.down))
            ChangeBehaviour("Falling");
        else if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            ChangeBehaviour("Rolling");
    }
}
