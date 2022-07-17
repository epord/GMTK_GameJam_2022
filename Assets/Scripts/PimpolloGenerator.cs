using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PimpolloGenerator : MonoBehaviour
{
    public HoldingZone holdingZone;
    public GameObject bushFace;
    private Shop shop;

    void Start()
    {
        shop = FindObjectOfType<Shop>();
        this.holdingZone = GetComponent<HoldingZone>();
        this.Generate();
    }

    public void Generate()
    {
        if (this.holdingZone.IsHoldingItem())
        {
            this.holdingZone.RemoveItem();
        }
        GameObject newBush = Instantiate(this.bushFace, this.transform);
        this.holdingZone.AssignItem(newBush.GetComponent<Grabbable>());
        BushFace bushFace = newBush.GetComponent<BushFace>();
        int max = Random.Range(3, 3 + shop.roundNumber);
        int attack = Random.Range(0, max+1);
        int defense = max - attack;
        bushFace.SetDiceFace(new DiceFace(DiceFace.Type.PIMPOLLO, attack, defense));
    }
}
