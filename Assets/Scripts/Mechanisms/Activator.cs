using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public abstract class Activator : MonoBehaviour
{
    [SerializeField]
    private List<Transform> m_children;

    public void SetChildrenActivation(bool activated)
    {
        foreach (Transform child in m_children)
        {
            Debug.Log("activated " + child);
            Mechanism mechanism = child.GetComponent<Mechanism>();
            if (mechanism != null)
            {
                Debug.Log("Found mechanism " + mechanism);
                mechanism.OnActivationChanged(activated);
            }
        }
    }
}
