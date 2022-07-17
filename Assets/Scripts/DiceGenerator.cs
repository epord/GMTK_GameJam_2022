using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceGenerator : MonoBehaviour
{
    private HoldingZone holdingZone;
    public GameObject dicePrefab;

    void Start()
    {
        this.holdingZone = GetComponent<HoldingZone>();    
    }

    void Update()
    {
        if (!this.holdingZone.IsHoldingItem())
        {
            GameObject newDice = Instantiate(dicePrefab, this.transform);
            this.holdingZone.AssignItem(newDice.GetComponent<Grabbable>());
        }
    }
}
