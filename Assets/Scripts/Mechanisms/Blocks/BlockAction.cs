using UnityEngine;
using System.Collections;

public abstract class BlockAction
{
    public float duration;

    public BlockAction()
    {

    }

    public abstract void Update();
}
