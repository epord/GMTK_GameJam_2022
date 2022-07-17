using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldingZone : MonoBehaviour
{
    protected GrabManager grabManager;
    public Grabbable holdedItem;
    public List<string> allowedTypes;

    public  void Start()
    {
        this.grabManager = FindObjectOfType<GrabManager>();
        if (this.holdedItem != null)
        {
            this.AssignItem(this.holdedItem);
        }
    }

    public virtual void AssignItem(Grabbable item)
    {
        if (item.holdingZone)
        {
            item.holdingZone.RemoveItem();
        }
        this.holdedItem = item;
        this.holdedItem.holdingZone = this;
        this.SnapHoldedItem();
    }

    public virtual bool CanHoldItemType(GameObject item)
    {
        return !this.IsHoldingItem() && allowedTypes.Contains(item.tag);
    }

    public void RemoveItem()
    {
        this.holdedItem.holdingZone = null;
        this.holdedItem = null;
    }

    public void SnapHoldedItem()
    {
        if (this.holdedItem != null)
        {
            Vector3 releaseZonePos = this.transform.position;
            this.holdedItem.transform.position = new Vector3(releaseZonePos.x, releaseZonePos.y, this.transform.position.z);
            this.holdedItem.isGrabbed = false;
        }
    }

    public bool IsHoldingItem()
    {
        return this.holdedItem != null;
    }

    private void OnMouseOver()
    {
        this.grabManager.SetReleaseZone(this);

        if (this.IsHoldingItem() && Input.GetMouseButtonDown(0))
        {
            this.grabManager.GrabObject(this.holdedItem);
        }
    }
}
