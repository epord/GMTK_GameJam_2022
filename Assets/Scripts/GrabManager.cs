using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabManager : MonoBehaviour
{
    public Grabbable holdedObject;
    public HoldingZone releaseZone;

    void Update()
    {
        if (this.IsHoldingObject() && Input.GetMouseButton(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10;
            Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePos);
            this.holdedObject.transform.position = new Vector3(worldMousePosition.x, worldMousePosition.y, this.holdedObject.transform.position.z);
        }
        else if (this.IsHoldingObject() && Input.GetMouseButtonUp(0)) {
            this.ReleaseObject();
        }
    }

    public void GrabObject(Grabbable o)
    {
        if (this.holdedObject != null)
        {
            throw new System.Exception("There is already an object being holded");
        }

        this.holdedObject = o;
        o.isGrabbed = true;
    }

    public void ReleaseObject()
    {
        if (!this.CanReleaseObject() || !this.IsHoldingObject())
        {
            return;
        }
        if (this.releaseZone.CanConsumeItem(this.holdedObject.gameObject))
        {
            this.releaseZone.ConsumeItem(this.holdedObject);
        }
        else if (this.releaseZone.CanHoldItemType(this.holdedObject.gameObject))
        {
            this.releaseZone.AssignItem(this.holdedObject);
        }
        else
        {
            this.holdedObject.holdingZone.AssignItem(this.holdedObject);
        }
        this.holdedObject = null;
    }

    public bool IsHoldingObject()
    {
        return this.holdedObject != null;
    }

    public void SetReleaseZone(HoldingZone zone)
    {
        this.releaseZone = zone;
    }

    public void UnsetReleaseZone()
    {
        if (this.holdedObject != null)
        {
            this.releaseZone = this.holdedObject.holdingZone;
        }
        else
        {
            this.releaseZone = null;
        }
    }

    public bool CanReleaseObject()
    {
        return this.releaseZone != null;
    }

}
