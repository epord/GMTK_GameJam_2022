using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnappingZone : HoldingZone
{
    public void Start()
    {
        base.Start();
    }

    private void OnMouseEnter()
    {
        this.grabManager.SetReleaseZone(this);
    }

    private void OnMouseExit()
    {
        this.grabManager.UnsetReleaseZone();
    }
}
