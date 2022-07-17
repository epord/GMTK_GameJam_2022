using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    public bool isGrabbed = false;
    public GrabManager grabManager;
    public HoldingZone holdingZone;

    void Start()
    {
        this.grabManager = FindObjectOfType<GrabManager>();
    }
}
