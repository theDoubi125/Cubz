using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 targetDistance;
    
	void Start ()
    {
        transform.position = target.position + targetDistance;
        transform.LookAt(target);
    }
	
	void Update ()
    {
        transform.position = target.position + targetDistance;
	}
}
