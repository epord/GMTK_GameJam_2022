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

    public bool CanConsumeItem(GameObject item)
    {
        return this.IsHoldingItem() && this.holdedItem.gameObject.tag == "Dice" && item.tag == "Face";
    }

    public void ConsumeItem(Grabbable item)
    {
        Debug.Log("Consume item");
        if (item != null && item.GetComponent<BushFace>() != null)
        {
            Debug.Log(item.GetComponent<BushFace>().diceFace.attack);
            Debug.Log(item.GetComponent<BushFace>().diceFace.defense);
        }
        Dice dice = this.holdedItem.GetComponent<Dice>();
        BushFace bushFace = item.GetComponent<BushFace>();
        if (dice != null && bushFace != null)
        {
            int idx = Random.Range(0, 6);
            dice.ReplaceFace(idx, bushFace.diceFace);

        }

        if (bushFace.GetComponent<Grabbable>() != null)
        {
            bushFace.GetComponent<Grabbable>().holdingZone.RemoveItem();
        }
        Destroy(item.gameObject);
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
            this.holdedItem.transform.position = new Vector3(releaseZonePos.x, releaseZonePos.y, -1);
            this.holdedItem.isGrabbed = false;
        }
    }

    public bool IsHoldingItem()
    {
        return this.holdedItem != null;
    }

    private void OnMouseOver()
    {
        if (this.IsHoldingItem() && Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse click");
            if (this.IsHoldingItem() && this.holdedItem.GetComponent<BushFace>() != null)
            {
                Debug.Log(this.holdedItem.GetComponent<BushFace>().diceFace.attack);
                Debug.Log(this.holdedItem.GetComponent<BushFace>().diceFace.defense);
            }
            this.grabManager.GrabObject(this.holdedItem);
        }
    }
    private void OnMouseEnter()
    {
        Debug.Log("Mouse enter");
        if (this.IsHoldingItem() && this.holdedItem.GetComponent<BushFace>() != null)
        {
            Debug.Log(this.holdedItem.GetComponent<BushFace>().diceFace.attack);
            Debug.Log(this.holdedItem.GetComponent<BushFace>().diceFace.defense);
        }
        this.grabManager.SetReleaseZone(this);
    }

    private void OnMouseExit()
    {
        this.grabManager.UnsetReleaseZone();
    }
}
