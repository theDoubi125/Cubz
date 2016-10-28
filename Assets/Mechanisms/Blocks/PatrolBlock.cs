using UnityEngine;
using System.Collections.Generic;

public class PatrolBlock : MovingBlock
{
    public List<Vector3> directions;
    private int currentPos = 0;
    private float pauseTime;
    public float pauseDuration;

	void Start ()
    {
        StartMovement(directions[currentPos]);
        currentPos++;
        if(currentPos >= directions.Count)
            currentPos = 0;
    }

    public override void UpdateMovement()
    {
        if (pauseTime > 0)
            pauseTime += Time.deltaTime;
        if(pauseTime > pauseDuration)
        {
            pauseTime = 0;
            StartMovement(directions[currentPos]);
            currentPos++;
            if(currentPos >= directions.Count)
                currentPos = 0;
        }
    }

    public override void OnMovementFinished()
    {
        pauseTime += Time.deltaTime;
    }
}
