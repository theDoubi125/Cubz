using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface LaserReceptor
{
    void OnLaserReceived(LaserSource source, RaycastHit hit);
    void OnLaserStopped(LaserSource source);
    void OnLaserUpdate(LaserSource source, RaycastHit hit);
}
