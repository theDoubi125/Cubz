using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public abstract class Activator : MonoBehaviour
{
    [SerializeField]
    public List<Transform> children;

    public void SetChildrenActivation(bool activated)
    {
        foreach (Transform child in children)
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
