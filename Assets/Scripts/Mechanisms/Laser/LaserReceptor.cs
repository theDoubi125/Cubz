using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface LaserReceptor
{
    void OnLaserReceived(LaserSource source);
    void OnLaserStopped(LaserSource source);
}
