using UnityEngine;
using System.Collections;

public class LaserSource : MonoBehaviour
{
    public float maxRange = 10, thickness = 0.1f;
    public Material laserMaterial;
    public Color laserColor;
    public bool activateAtStart;

    private bool _activated;
    private LineRenderer lineRenderer;

    public bool activated
    {
        get
        {
            return _activated;
        }
        set
        {
            bool mustUpdate = false;
            if (!value && _activated)
                mustUpdate = true;
            _activated = value;
            if (mustUpdate && currentTarget != null)
            {
                currentTarget.OnLaserStopped(this);
                currentTarget = null;
            }
        }
    }
    private LaserReceptor currentTarget;

    private Vector3 _laserDirection;
    public Vector3 laserDirection
    {
        get
        {
            return customDirection ? _laserDirection : transform.right;
        }
        set
        {
            customDirection = true;
            _laserDirection = value;
        }
    }

    public Vector3 laserStart;
    public bool customDirection = false;

    public void Init(float thickness, Material material, Color color, bool active)
    {
        this.thickness = thickness;
        laserMaterial = material;
        laserColor = color;
        activateAtStart = active;
    }

	void Start ()
    {
        activated = activateAtStart;
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.SetColors(laserColor, laserColor);
        lineRenderer.SetVertexCount(2);
        lineRenderer.SetPosition(0, Vector3.zero);
        lineRenderer.SetPosition(1, Vector3.zero);
        lineRenderer.SetWidth(thickness, thickness);
        lineRenderer.material = laserMaterial;
        UpdateLaserColor();
	}
	
	void Update ()
    {
        lineRenderer.SetPosition(0, transform.position + laserStart);
        if (!activated || laserColor == Color.black)
        {
            lineRenderer.SetPosition(1, transform.position + laserStart);
            return;
        }
        Vector3 direction = customDirection ? laserDirection : transform.right;
        Ray ray = new Ray(transform.position + laserStart + direction*0.01f, direction);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxRange))
        {
            lineRenderer.SetPosition(1, hit.point);
            LaserReceptor receptor = hit.transform.GetComponent<LaserReceptor>();
            if (receptor != currentTarget)
            {
                if (currentTarget != null)
                    currentTarget.OnLaserStopped(this);
                if (receptor != null)
                    receptor.OnLaserReceived(this, hit);
                currentTarget = receptor;
            }
            else if(currentTarget != null)
                currentTarget.OnLaserUpdate(this, hit);
        }
        else
            lineRenderer.SetPosition(1, transform.position + laserStart + transform.right * maxRange);
	}

    public void SetLaserDirection(Vector3 dir)
    {
        customDirection = true;
        laserDirection = dir;
    }

    public void UpdateLaserColor()
    {
        lineRenderer.SetColors(laserColor, laserColor);
    }
        
}
