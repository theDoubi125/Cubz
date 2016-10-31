using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float pitch, yaw, distance;
    
	void Start ()
    {
        
    }
	
	void Update ()
    {
        UpdateCamera();
	}

    void UpdateCamera()
    {
        Vector3 vecPos = new Vector3(Mathf.Cos(pitch) * Mathf.Cos(yaw), Mathf.Sin(yaw), Mathf.Sin(pitch) * Mathf.Cos(yaw));
        Ray ray = new Ray(target.position, vecPos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, distance))
            transform.position = hit.point;
        else
            transform.position = target.position + vecPos * distance;
        transform.LookAt(target.position);
    }
}
