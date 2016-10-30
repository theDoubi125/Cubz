using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColDetector : MonoBehaviour
{
    public delegate void CollisionDelegate(Collider collider);

    CollisionDelegate OnOverlap;
    public Vector3 offset;

	void Start ()
    {
	
	}
	
	void Update ()
    {
        transform.position = transform.parent.position + offset;
	}

    void OnTriggerEnter(Collider other)
    {
        OnOverlap(other);
    }

    public void AddListener(CollisionDelegate f)
    {
        OnOverlap += f;
    }

    public void SetOffset(Vector3 offset)
    {
        this.offset = offset; 
    }
}
