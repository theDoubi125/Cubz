using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler
{
    private int collisionMask;
    private Transform target;

    public CollisionHandler(Transform target, int collisionMask)
    {
        this.target = target;
        this.collisionMask = collisionMask;
    }

    public Collider[] GetCollidingEntities(int x, int y, int z)
    {
		//return Physics.OverlapBox(transform.position + Vector3.right * x + Vector3.up * y + Vector3.forward * z, new Vector3(0.499f, 0.499f, 0.499f));
		return Physics.OverlapBox(target.position + Vector3.right * x + Vector3.up * y + Vector3.forward * z, new Vector3(0.499f, 0.499f, 0.499f), Quaternion.identity, collisionMask);
    }

    public bool IsCellEmpty(int x, int y, int z)
    {
        return GetCollidingEntities(x, y, z).Length == 0;
    }

    public Collider[] GetCollidingEntities(Vector3 pos)
    {
		//return Physics.OverlapBox(transform.position + Vector3.right * pos.x + Vector3.up * pos.y + Vector3.forward * pos.z, new Vector3(0.499f, 0.499f, 0.499f));
        return Physics.OverlapBox(target.position + Vector3.right * pos.x + Vector3.up * pos.y + Vector3.forward * pos.z, new Vector3(0.499f, 0.499f, 0.499f), Quaternion.identity, collisionMask);
    }

    public bool IsCellEmpty(Vector3 pos)
    {
        return GetCollidingEntities(pos).Length == 0;
    }
}
