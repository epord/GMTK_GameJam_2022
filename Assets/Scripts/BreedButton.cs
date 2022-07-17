using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (this.input1.IsHoldingItem() && this.input2.IsHoldingItem() && Input.GetMouseButtonDown(0))
        {
            GameObject newDice = Instantiate(dicePrefab, new Vector3(0, 0, 0), Quaternion.identity);
            this.output.AssignItem(newDice.GetComponent<Grabbable>());
        }
    }
}
