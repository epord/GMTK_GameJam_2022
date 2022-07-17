using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BreedButton : MonoBehaviour
{
    public HoldingZone input1;
    public HoldingZone input2;
    public HoldingZone output;
    public GameObject dicePrefab;

    public Sprite spriteEnabled;
    public Sprite SpriteDisabled;
    private SpriteRenderer renderer;

    private void Start()
    {
        this.renderer = GetComponent<SpriteRenderer>();
        this.renderer.sprite = SpriteDisabled;
    }

    private void Update()
    {
        if (this.input1.IsHoldingItem() && this.input2.IsHoldingItem())
        {
            this.renderer.sprite = spriteEnabled;
        }
        else
        {
            this.renderer.sprite = SpriteDisabled;
        }
    }

    private void OnMouseOver()
    {
        if (this.input1.IsHoldingItem() && this.input2.IsHoldingItem() && !this.output.IsHoldingItem() && Input.GetMouseButtonDown(0))
        {
            Dice dice1 = this.input1.holdedItem.GetComponent<Dice>();
            Dice dice2 = this.input2.holdedItem.GetComponent<Dice>();

            GameObject newDice = Instantiate(dicePrefab, new Vector3(0, 0, 0), Quaternion.identity);
            this.BreedDice(dice1.dicefaces, dice2.dicefaces, newDice.GetComponent<Dice>());

            this.output.AssignItem(newDice.GetComponent<Grabbable>());
        }
    }

    private void BreedDice(DiceFace[] daddy, DiceFace[] mommy, Dice result)
    {
        DiceFace[] daddyClone = (DiceFace[])daddy.Clone();
        DiceFace[] mommyClone = (DiceFace[])mommy.Clone();

        Array.Sort(daddyClone, (x, y) => UnityEngine.Random.RandomRange(-10, 10));
        Array.Sort(mommyClone, (x, y) => UnityEngine.Random.RandomRange(-10, 10));

        result.dicefaces = new DiceFace[] { daddyClone[0], daddyClone[1], daddyClone[2], mommyClone[0], mommyClone[1], mommyClone[2]};
    }
}
