using UnityEngine;
using System.Collections.Generic;

public class PatrolBlock : MovingBlock
{
    public List<Vector3> directions;
    int currentPos = 0, currentIt = 0;

	void Start ()
    {

    }
	
	public override Vector3 GetNextDir()
    {
        Vector3 result = directions[currentPos];
        if(currentIt == 0)
            Pause(2);
        currentIt++;
        if(currentIt >= directions[currentPos].magnitude)
        {
            currentIt = 0;
            currentPos++;
            if (currentPos >= directions.Count)
                currentPos = 0;
        }
        return result.normalized;
	}
}
