using UnityEngine;
using System.Collections;

public class PatrolBlock : MovingBlock
{
    public Vector3[] targets;
    public float travelSpeed;
    public int initTarget;
    public float waitTime;
    private bool isWaiting;
    private int currentTarget;
    private float currentTime;

	void Start ()
    {
        currentTarget = (initTarget+1)%targets.Length;
        transform.position = targets[lastTarget];
        isWaiting = true;
    }
	
	public override void UpdateBlock ()
    {
        if(isWaiting)
        {
            SetVelocity(Vector3.zero);
            currentTime += Time.deltaTime;
            if (currentTime > waitTime)
            {
                currentTime = 0;
                isWaiting = false;
            }
        }
        else
        {
            Vector3 traj = (targets[currentTarget] - targets[lastTarget]);
            if (currentTime + Time.deltaTime > traj.magnitude)
            {
                float deltaTime = traj.magnitude - currentTime;
                // Hack to get always exact positions (same as with cube movements)
                SetVelocity((targets[currentTarget]-transform.position) / Time.deltaTime);
                currentTarget++;
                if (currentTarget >= targets.Length)
                    currentTarget = 0;
                currentTime = 0;
                isWaiting = true;
            }
            else
            {
                currentTime += Time.deltaTime;
                SetVelocity(travelSpeed * traj.normalized);
            }
        }
	}

    private int lastTarget
    {
        get
        {
            if (currentTarget == 0)
                return targets.Length - 1;
            return currentTarget - 1;
        }
    }
}
