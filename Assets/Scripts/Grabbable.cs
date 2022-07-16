using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    public bool isGrabbed = false;
    public GrabManager grabManager;

    void Start()
    {
        this.grabManager = FindObjectOfType<GrabManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isGrabbed && Input.GetMouseButtonDown(0))
        {
            this.grabManager.ReleaseObject();
        }
        else if (this.isGrabbed)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10;
            Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePos);
            this.transform.position = new Vector3(worldMousePosition.x, worldMousePosition.y, this.transform.position.z);
        }
    }

    private void OnMouseOver()
    {
        if (!this.isGrabbed && Input.GetMouseButtonDown(0))
        {
            this.grabManager.GrabObject(this);
        }
    }
}
