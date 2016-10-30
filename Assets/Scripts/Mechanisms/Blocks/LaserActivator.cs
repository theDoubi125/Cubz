using UnityEngine;
using System.Collections.Generic;

public class LaserActivator : Activator, LaserReceptor
{
    public Color targetColor;
    private List<LaserSource> alims = new List<LaserSource>();

    public void OnLaserReceived(LaserSource source, RaycastHit hit)
    {
        if (source.laserColor == targetColor)
        {
            if (alims.Count == 0)
                SetChildrenActivation(true);
            alims.Add(source);

        }
    }

    public void OnLaserStopped(LaserSource source)
    {
        alims.Remove(source);
        if (alims.Count == 0)
            SetChildrenActivation(false);
    }

    public void OnLaserUpdate(LaserSource source, RaycastHit hit)
    {

    }

}

