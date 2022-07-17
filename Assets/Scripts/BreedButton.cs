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

    public Sprite[] breedSignSprites = new Sprite[4];
    public Sprite[] breedHoverSprites = new Sprite[4];
    public Sprite[] breedDisableSprites = new Sprite[4];
    private SpriteRenderer renderer;

    public int breedRemaining = 3;
    private bool hovering = false;

    private void Start()
    {
        this.renderer = GetComponent<SpriteRenderer>();
    }

    private bool isDisabled()
    {
        return !this.input1.IsHoldingItem() || !this.input2.IsHoldingItem() || this.output.IsHoldingItem() || breedRemaining == 0;
    }

    private void Update()
    {
        if (isDisabled())
        {
            this.renderer.sprite = breedDisableSprites[breedRemaining];
        }
        else if (hovering && !Input.GetMouseButton(0))
        {
            this.renderer.sprite = breedHoverSprites[breedRemaining];
        } else
        {
            this.renderer.sprite = breedSignSprites[breedRemaining];
        }
    }

    private void OnMouseExit()
    {
        hovering = false;
    }

    private void OnMouseOver()
    {
        hovering = true;
        if (!isDisabled() && Input.GetMouseButtonUp(0))
        {
            Dice dice1 = this.input1.holdedItem.GetComponent<Dice>();
            Dice dice2 = this.input2.holdedItem.GetComponent<Dice>();

            GameObject newDice = Instantiate(dicePrefab, new Vector3(0, 0, 0), Quaternion.identity);
            this.BreedDice(dice1.diceFaces, dice2.diceFaces, newDice.GetComponent<Dice>());

            this.output.AssignItem(newDice.GetComponent<Grabbable>());
        }
    }

    private void BreedDice(DiceFace[] daddy, DiceFace[] mommy, Dice result)
    {
        DiceFace[] daddyClone = (DiceFace[])daddy.Clone();
        int[] indeces = {0, 1, 2, 3, 4, 5};
        for (int i = 0; i < 7; i++)
        {
            Array.Sort(indeces, (x, y) => UnityEngine.Random.RandomRange(-10, 10));
        }
        for (int i = 0; i < 3; i++)
        {
            daddyClone[indeces[i]] = mommy[indeces[i]];
            StartCoroutine(result.PLayMommy(indeces[i]));
        }
        
        for (int i = 3; i < 6; i++)
        {
            StartCoroutine(result.PLayDaddy(indeces[i]));
        }
        result.SetDiceFaces(daddyClone);
        
        breedRemaining -= 1;
    }
}
