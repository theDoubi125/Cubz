using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RollingBehaviour : CubeBehaviour
{
    private bool climbing;

    public override void OnStart(CubeController cube)
    {
        Debug.Log("START ROLLING");
        Vector3 currentDir;
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > Mathf.Abs(Input.GetAxis("Vertical")))
            currentDir = (cube.right * Input.GetAxis("Horizontal")).normalized;
        else
            currentDir = (cube.forward * Input.GetAxis("Vertical")).normalized;

        if(cube.colHandler.IsCellEmpty(currentDir))
        {
            climbing = false;
            StartRotation(cube, currentDir + Vector3.down, currentDir);
        }
        else
        {
            climbing = true;
            StartRotation(cube, currentDir + Vector3.up, currentDir);
        }
        SetRotationSpeed(cube, 1);
    }

    public override void OnGround(CubeController cube)
    {
        if (!climbing && cube.colHandler.IsCellEmpty(Vector3.down))
            ChangeBehaviour(cube, "Falling");
        else if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            OnStart(cube);
        else ChangeBehaviour(cube, "Standing");
    }

    public override void UpdateBehaviour(CubeController cube)
    {

    }
}