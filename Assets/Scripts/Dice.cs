using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Dice : MonoBehaviour
{
    [SerializeField]
    public DiceFace[] dicefaces;

    public DiceFace selectedFace;
    public Transform[] flowerLocations;
    public GameObject bushFacePrefab;

    public bool alive;

    void Awake()
    {
        this.dicefaces = new DiceFace[6];
        for (int i = 0; i < 6; i++)
        {
            this.dicefaces[i] = new DiceFace(DiceFace.Type.PIMPOLLO, 1, 1);
            GameObject newBushFace = Instantiate(bushFacePrefab, this.flowerLocations[i].transform);
            newBushFace.GetComponent<BushFace>().SetDiceFace(this.dicefaces[i]);
        }

    }


}
