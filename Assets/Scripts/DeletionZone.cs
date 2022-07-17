using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletionZone : HoldingZone
{
    public override void AssignItem(Grabbable item)
    {
        base.AssignItem(item);
        this.RemoveItem();
        DestroyImmediate(item.gameObject);
    }
}
