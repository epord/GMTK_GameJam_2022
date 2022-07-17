using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyButton : MonoBehaviour
{
  
    private BattleController battleController;
    private Shop shop;
    private GameObject shopObject;
    private bool readyEnabled = false;

    public Sprite readyEnabledSprite;
    public Sprite readyDisabledSprite;
    
    private void Start()
    {
        battleController = FindObjectOfType<BattleController>();
        shop = FindObjectOfType<Shop>();
        shopObject = shop.gameObject;
    }

    private void Update()
    {
        readyEnabled = true;
        foreach (var holdingZone in shop.bank)
        {
            if (holdingZone.holdedItem == null)
            {
                readyEnabled = false;
                break;
            }
        }

        if (readyEnabled) GetComponent<SpriteRenderer>().sprite = readyEnabledSprite;
        else GetComponent<SpriteRenderer>().sprite = readyDisabledSprite;

    }

    private void OnMouseOver()
    {
        if (readyEnabled)
        {
            if (Input.GetMouseButtonDown(0))
            {
                shopObject.SetActive(false);
                battleController.GenerateEnemies(4, 1);
            }
        }
    }
}
