using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RollingBehaviour : CubeBehaviour
{
    public override void OnStart(CubeController cube)
    {
        Debug.Log("START ROLLING");
        Vector3 currentDir;
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > Mathf.Abs(Input.GetAxis("Vertical")))
            currentDir = (cube.right * Input.GetAxis("Horizontal")).normalized;
        else
            currentDir = (cube.forward * Input.GetAxis("Vertical")).normalized;
        Debug.Log(currentDir);
        if(cube.colHandler.IsCellEmpty(currentDir))
            StartRotation(cube, currentDir + Vector3.down, currentDir);
        else
            StartRotation(cube, currentDir + Vector3.up, currentDir);
        SetRotationSpeed(cube, 1);
    }

    public override void OnGround(CubeController cube)
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            OnStart(cube);
        else ChangeBehaviour(cube, "Standing");
    }

    public override void UpdateBehaviour(CubeController cube)
    {

    }
}