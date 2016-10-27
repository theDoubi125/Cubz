using UnityEngine;
using System.Collections;

public class DoorBlock : MovingBlock, Mechanism
{
    public Vector3 translationVector;
    private bool mustAnim, canActivate = true, activated;

    public override void UpdateMovement()
    {
        if (mustAnim && canActivate)
        {
            StartMovement(translationVector * (activated ? 1 : -1));
            activated = !activated;
            canActivate = false;
            mustAnim = false;
        }
    }

    public override void OnMovementFinished()
    {
        canActivate = true;
    }

    public void OnActivationChanged(bool activated)
    {
        Debug.Log("Activated door : " + activated);
        mustAnim = activated != this.activated;
    }
}
