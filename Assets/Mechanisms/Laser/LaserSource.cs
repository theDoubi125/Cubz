using UnityEngine;
using System.Collections;

public class LaserSource : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public float maxRange = 10;
    public Color laserColor;
    public bool activated;

	void Start ()
    {
        lineRenderer = GetComponent<LineRenderer>();
	}
	
	void Update ()
    {
        lineRenderer.SetPosition(0, transform.position);
        if (!activated)
        {
            lineRenderer.SetPosition(1, transform.position);
            return;
        }
        Ray ray = new Ray(transform.position, transform.right);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxRange))
        {
            lineRenderer.SetPosition(1, hit.point);
            LaserReceptor receptor = hit.transform.GetComponent<LaserReceptor>();
            if (receptor)
                receptor.OnLaserHit(laserColor);
        }
        else
            lineRenderer.SetPosition(1, transform.position + transform.right * maxRange);
	}
}
