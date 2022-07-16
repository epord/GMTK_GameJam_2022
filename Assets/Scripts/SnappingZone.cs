using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnappingZone : MonoBehaviour
{
    private GrabManager grabManager;
    private void Start()
    {
        this.grabManager = FindObjectOfType<GrabManager>();
    }

    private void OnMouseEnter()
    {
        this.grabManager.SetReleaseZone(this);
    }

    private void OnMouseOver()
    {
        this.grabManager.SetReleaseZone(this);
    }

    private void OnMouseExit()
    {
        this.grabManager.UnsetReleaseZone();
    }
}
