using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnappingZone : HoldingZone
{
    public void Start()
    {
        base.Start();
    }

    private void Update()
    {
        if (holdedItem != null)
        {
            SpriteRenderer renderer = this.gameObject.GetComponent<SpriteRenderer>();
            if (renderer != null) renderer.enabled = false;
        }
        else
        {
            SpriteRenderer renderer = this.gameObject.GetComponent<SpriteRenderer>();
            if (renderer != null) renderer.enabled = true;
        }
    }
}
