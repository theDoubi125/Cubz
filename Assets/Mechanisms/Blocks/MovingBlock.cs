using UnityEngine;
using System.Collections.Generic;

public abstract class MovingBlock : MonoBehaviour
{
    private Vector3 velocity;

    private List<Transform> attachedCubes = new List<Transform>();

    void Update()
    {
        UpdateBlock();
        transform.position += Time.deltaTime * velocity;
        foreach(Transform cube in attachedCubes)
        {
            cube.position += Time.deltaTime * velocity;
        }
    }

    public abstract void UpdateBlock();

    public void AttachCube(Transform cube)
    {
        attachedCubes.Add(cube);
    }

    public void DetachCube(Transform cube)
    {
        attachedCubes.Remove(cube);
    }

    protected void SetVelocity(Vector3 velocity)
    {
        this.velocity = velocity;
    }
}
