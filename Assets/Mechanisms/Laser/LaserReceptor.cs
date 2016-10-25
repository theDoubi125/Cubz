using UnityEngine;
using System.Collections;

public abstract class LaserReceptor : MonoBehaviour
{
    public Color color;

    public void OnLaserHit(Color color)
    {
        if (color == this.color)
        {
            OnActivation();
        }
    }

    public abstract void OnActivation();
}
