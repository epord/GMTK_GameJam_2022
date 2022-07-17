using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{

    public HoldingZone[] bank = new HoldingZone[8];
    public PimpolloGenerator[] PimpolloGenerators = new PimpolloGenerator[6];
    public HoldingZone[] cleanZones;

    public int roundNumber = 1;

    public void RefreshShop()
    {
        roundNumber += 1;
        GetComponentInChildren<BreedButton>().breedRemaining = 3;
        CleanZones();
        GeneratePimpollo();
    }

    private void CleanZones()
    {
        foreach (var cleanZone in cleanZones)
        {
            if (cleanZone.IsHoldingItem())
            {
                var holdedItem = cleanZone.holdedItem;
                cleanZone.RemoveItem();
                Destroy(holdedItem.gameObject);
            }
        }
    }

    private void GeneratePimpollo()
    {
        foreach (var pimpolloGenerator in PimpolloGenerators)
        {
            pimpolloGenerator.Generate();
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        DiceGenerator diceGenerator = FindObjectOfType<DiceGenerator>();
        foreach (var holdingZone in bank)
        {
            GameObject newDice = Instantiate(diceGenerator.dicePrefab, this.transform);
            holdingZone.AssignItem(newDice.GetComponent<Grabbable>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
