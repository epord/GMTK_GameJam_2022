using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Dice : MonoBehaviour
{
    [SerializeField]
    public DiceFace[] dicefaces;

    public DiceFace selectedFace;

    void Awake()
    {
        this.dicefaces = new DiceFace[6];
        for (int i = 0; i < 6; i++)
        {
            this.dicefaces[i] = new DiceFace(Random.RandomRange(0, 10), Random.RandomRange(0, 10));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
