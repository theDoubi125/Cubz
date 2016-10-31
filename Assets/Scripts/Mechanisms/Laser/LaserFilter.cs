using System;
using System.Collections.Generic;
using UnityEngine;

public class LaserFilter : LaserSource, LaserReceptor
{
    private List<LaserSource> InLasers = new List<LaserSource>();

    public Color FilterColor;

    public void OnLaserReceived(LaserSource source, RaycastHit hit)
    {
        activated = true;
        InLasers.Add(source);
        SetLaserDirection(source.laserDirection);
        laserStart = hit.point - transform.position;
        UpdateLaser();
    }

    public void OnLaserStopped(LaserSource source)
    {
        InLasers.Remove(source);
        UpdateLaser();
    }

    public void OnLaserUpdate(LaserSource source, RaycastHit hit)
    {
        laserStart = hit.point - transform.position;
        SetLaserDirection(source.laserDirection);
    }

    public void UpdateLaser()
    {
        if (InLasers.Count == 0)
        {
            activated = false;
            return;
        }
        Color resultColor = InLasers[0].laserColor;

        resultColor.r *= FilterColor.r;
        resultColor.g *= FilterColor.g;
        resultColor.b *= FilterColor.b;
        laserColor = resultColor;

        UpdateLaserColor();
    }
}

