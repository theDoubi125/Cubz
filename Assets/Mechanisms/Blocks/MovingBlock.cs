using UnityEngine;
using System.Collections.Generic;

public abstract class MovingBlock : MonoBehaviour
{
    public float speed;
    private Vector3 direction;
    private float time, duration, pauseDuration;

    private List<CubeController> waitForAttach = new List<CubeController>();
    private List<CubeController> waitForDetach = new List<CubeController>();
    private List<CubeController> attachedCubes = new List<CubeController>();


    void Start()
    {
        direction = GetNextDir();
    }

    void Update()
    {
        if(pauseDuration > 0)
        {
            pauseDuration -= Time.deltaTime;
            UpdateAttach();
            return;
        }
        if (time + Time.deltaTime > duration)
        {
            float deltaTime = duration - time;
            Move(deltaTime * speed * direction);
            Vector3 newDir = GetNextDir();
            duration = newDir.magnitude / speed;
            direction = newDir.normalized;
            time = 0;
            UpdateAttach();
        }
        else
        {
            Move(Time.deltaTime * speed * direction);
            time += Time.deltaTime;
        }
    }

    public abstract Vector3 GetNextDir();

    private void Move(Vector3 disp)
    {
        transform.position += disp;
        foreach (CubeController cube in attachedCubes)
        {
            cube.transform.position += disp;
        }
    }

    public void AttachCube(CubeController cube)
    {
        if(!waitForAttach.Contains(cube))
            waitForAttach.Add(cube);
        waitForDetach.Remove(cube);
    }

    public void DetachCube(CubeController cube)
    {
        if (!waitForDetach.Contains(cube))
            waitForDetach.Add(cube);
        waitForAttach.Remove(cube);
    }

    public void UpdateAttach()
    {
        foreach (CubeController cube in waitForAttach)
        {
            if (!attachedCubes.Contains(cube))
                attachedCubes.Add(cube);
        }
        foreach(CubeController cube in waitForDetach)
            attachedCubes.Remove(cube);
        waitForAttach.Clear();
        waitForDetach.Clear();
    }

    public void Pause(float duration)
    {
        pauseDuration = duration;
    }
}
