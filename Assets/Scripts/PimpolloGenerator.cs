using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PimpolloGenerator : MonoBehaviour
{
    public HoldingZone holdingZone;
    public GameObject bushFace;

    void Start()
    {
        this.holdingZone = GetComponent<HoldingZone>();
        this.Generate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Generate()
    {
        if (!this.holdingZone.IsHoldingItem())
        {
            GameObject newBush = Instantiate(this.bushFace, this.transform);
            this.holdingZone.AssignItem(newBush.GetComponent<Grabbable>());
            BushFace bushFace = newBush.GetComponent<BushFace>();
            int attack = Random.Range(0, 5);
            bushFace.SetDiceFace(new DiceFace(DiceFace.Type.PIMPOLLO, attack, 5 - attack)); //TODO: funciton de DIEGO
        }
    }
}
