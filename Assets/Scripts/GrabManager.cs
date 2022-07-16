using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabManager : MonoBehaviour
{
    public Grabbable holdedItem;
    public SnappingZone releaseZone;

    public void GrabObject(Grabbable o)
    {
        if (this.holdedItem != null)
        {
            throw new System.Exception("There is already an object being holded");
        }

        this.holdedItem = o;
        o.isGrabbed = true;
        o.gameObject.layer = 2; // Move to "Ignore Raycast" layer
    }

    public void ReleaseObject()
    {
        if (!this.CanReleaseObject())
        {
            return;
        }

        this.holdedItem.gameObject.layer = 0; // Move to "Default" layer
        Vector3 releaseZonePos = this.releaseZone.transform.position;
        this.holdedItem.transform.position = new Vector3(releaseZonePos.x, releaseZonePos.y, this.transform.position.z);
        this.holdedItem.isGrabbed = false;
        this.holdedItem = null;
    }

    public bool IsHoldingObject()
    {
        return this.holdedItem != null;
    }

    public void SetReleaseZone(SnappingZone zone)
    {
        this.releaseZone = zone;
    }

    public void UnsetReleaseZone()
    {
        this.releaseZone = null;
    }

    public bool CanReleaseObject()
    {
        return this.releaseZone != null;
    }

}
