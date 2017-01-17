using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RollingBehaviour : CubeBehaviour
{
    private bool m_climbing, m_lastClimb;
    private Vector3 m_currentDir;
    private float m_currentSpeed;

    public override void OnStart()
    {
        SetMovementDistance(1);
        m_currentSpeed = Time.deltaTime * m_cube.acceleration;
        InitDir();
    }

    public override void OnEnd()
    {
        m_currentSpeed = 0;
        m_climbing = false;
        m_lastClimb = false;
    }

    public void InitDir()
    {
        m_currentSpeed = Mathf.Abs(m_currentSpeed);
        if(!m_climbing)
        {
            m_currentDir = m_cube.GetMainInputDirection();
        }
        m_lastClimb = m_climbing;
        if (m_cube.colHandler.IsCellEmpty(Vector3.up))
        {
            if (m_cube.colHandler.IsCellEmpty(m_currentDir) && m_cube.colHandler.IsCellEmpty(m_currentDir + Vector3.up))
            {
                m_climbing = false;
                SetMovementDistance(1);
                StartRotation(m_currentDir + Vector3.down, m_currentDir);
                SetRotationSpeed(m_currentSpeed);
            }
            else if ((!m_climbing || m_cube.colHandler.IsCellEmpty(m_currentDir)) && m_cube.colHandler.IsCellEmpty(-m_currentDir) && m_cube.colHandler.IsCellEmpty(-m_currentDir + Vector3.up))
            {
                m_climbing = true;
                m_lastClimb = false;
                StartRotation(m_currentDir + Vector3.up, m_currentDir);
                SetRotationSpeed(m_currentSpeed);
            }
            else if (m_cube.colHandler.IsCellEmpty(Vector3.down))
            {
                ChangeBehaviour("Falling");
            }
            else ChangeBehaviour("Standing");

        }
        else ChangeBehaviour("Standing");
    }

    public override void OnGround()
    {
        if (!m_climbing && m_cube.colHandler.IsCellEmpty(Vector3.down))
            ChangeBehaviour("Falling");
        else if (m_cube.GetMainInputDirection().sqrMagnitude > 0 || m_climbing)
            InitDir();
        else ChangeBehaviour("Standing");
    }

    public override void OnBack()
    {
        if(m_lastClimb)
        {
            m_cube.ReverseMovement();
            m_climbing = true;
            m_lastClimb = false;
        }
        else
            OnGround();
    }

    public override void UpdateBehaviour()
    {
        m_currentSpeed += Time.deltaTime * m_cube.acceleration * m_cube.GetInputWork();
        if (m_climbing)
            m_currentSpeed -= m_cube.gravity * Time.deltaTime;
        else
        {
            if (m_cube.currentPos < 0.5f)
                m_currentSpeed -= m_cube.gravity * Time.deltaTime;
            else m_currentSpeed += m_cube.gravity * Time.deltaTime;
        }
        if (m_currentSpeed > m_cube.maxSpeed)
            m_currentSpeed = m_cube.maxSpeed;
        SetRotationSpeed(m_currentSpeed);
    }
}