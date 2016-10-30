using System;
using System.Collections.Generic;
using UnityEngine;

public class LaserReflector : LaserSource, LaserReceptor
{
    private List<LaserSource> InLasers = new List<LaserSource>();

    public void OnLaserReceived(LaserSource source)
    {
        activated = true;
        InLasers.Add(source);
        UpdateColor();
    }

    public void OnLaserStopped(LaserSource source)
    {
        InLasers.Remove(source);
        UpdateColor();
    }

    public void UpdateColor()
    {
        if (InLasers.Count == 0)
        {
            activated = false;
            return;
        }
        Color resultColor = new Color(0, 0, 0);
        foreach (LaserSource source in InLasers)
        {
            resultColor.r += source.laserColor.r;
            resultColor.g += source.laserColor.g;
            resultColor.b += source.laserColor.b;
        }
        laserColor = resultColor;
        UpdateLaserColor();
    }
}

