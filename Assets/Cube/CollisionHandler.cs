using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.3f);

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    if (GetCollidingEntities(i - 1, j - 1, k - 1).Length > 0)
                        Gizmos.DrawCube(transform.position + Vector3.right * (i - 1) + Vector3.up * (j - 1) + Vector3.forward * (k - 1), new Vector3(1.01f, 1.01f, 1.01f));
                }
            }
        }
    }

    void Update()
    {
        
    }

    public Collider[] GetCollidingEntities(int x, int y, int z)
    {
        return Physics.OverlapBox(transform.position + Vector3.right * x + Vector3.up * y + Vector3.forward * z, new Vector3(0.499f, 0.499f, 0.499f));
    }

    public bool IsCellEmpty(int x, int y, int z)
    {
        return GetCollidingEntities(x, y, z).Length == 0;
    }

    public Collider[] GetCollidingEntities(Vector3 pos)
    {
        return Physics.OverlapBox(transform.position + Vector3.right * pos.x + Vector3.up * pos.y + Vector3.forward * pos.z, new Vector3(0.499f, 0.499f, 0.499f));
    }

    public bool IsCellEmpty(Vector3 pos)
    {
        return GetCollidingEntities(pos).Length == 0;
    }
}
