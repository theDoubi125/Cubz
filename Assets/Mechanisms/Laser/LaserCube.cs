using UnityEngine;
using System.Collections;

public class LaserCube : LaserReceptor
{
    public override void OnActivation()
    {
        print("OnActivation!!!");
        GetComponent<LaserSource>().activated = true;
    }
}
