using System;
using System.Collections.Generic;
using UnityEngine;

public class LaserFilter : MonoBehaviour, LaserReceptor
{
    private List<LaserSource> InLasers = new List<LaserSource>();
    private List<LaserSource> OutLasers = new List<LaserSource>();

    private Dictionary<LaserSource, LaserSource> lasers = new Dictionary<LaserSource, LaserSource>();

    public Color FilterColor;

    public void OnLaserReceived(LaserSource source, RaycastHit hit)
    {
        InLasers.Add(source);
        AddLaser(source, hit);
    }

    public void OnLaserStopped(LaserSource source)
    {
        InLasers.Remove(source);
        GameObject.Destroy(lasers[source].gameObject);
        lasers.Remove(source);
    }

    public void OnLaserUpdate(LaserSource source, RaycastHit hit)
    {
        lasers[source].transform.position = hit.point;
        lasers[source].SetLaserDirection(source.laserDirection);
    }

    public void AddLaser(LaserSource inLaser, RaycastHit hit)
    {
        GameObject obj = new GameObject();
        obj.transform.parent = transform;
        obj.transform.position = hit.point;
        LaserSource outLaser = obj.AddComponent<LaserSource>();
        Color color = new Color(
                          inLaser.laserColor.r * FilterColor.r,
                          inLaser.laserColor.g * FilterColor.g,
                          inLaser.laserColor.b * FilterColor.b);
            
        outLaser.Init(inLaser.thickness, inLaser.laserMaterial, color, true);
        lasers[inLaser] = outLaser;
    }
}

