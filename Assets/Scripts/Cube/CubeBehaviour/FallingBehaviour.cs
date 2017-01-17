using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBehaviour : CubeBehaviour
{
    private float m_currentSpeed;

    public override void OnGround()
    {
        SetMovementDistance(1);
        if (!m_cube.colHandler.IsCellEmpty(Vector3.down))
            ChangeBehaviour("Standing");
    }

    public override void OnBack()
    {
        OnGround();
    }

    public override void OnStart()
    {
        SetTranslation(Vector3.down);
        StartRotation(Vector3.zero, Vector3.zero);
        m_currentSpeed = 0;
    }

    public override void OnEnd()
    {
        SetTranslation(Vector3.zero);
    }

    public override void UpdateBehaviour()
    {
        m_currentSpeed += m_cube.gravity * Time.deltaTime;
        SetRotationSpeed(m_currentSpeed);

    }
}
