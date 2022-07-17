using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyButton : MonoBehaviour
{
  
    private BattleController battleController;
    private Shop shop;
    private GameObject shopObject;
    private bool readyEnabled = false;

    public Sprite[] readySprites = new Sprite[9];
    public Sprite hoveringSprite;

    private SoundManager soundManager;

    private bool hovering;
    
    private bool firstTime = true;

    private void Awake()
    {
        battleController = FindObjectOfType<BattleController>();
        shop = FindObjectOfType<Shop>();
        shopObject = shop.gameObject;
        this.soundManager = FindObjectOfType<SoundManager>();
    }

    private void Update()
    {
        int bushes = 0;
        foreach (var holdingZone in shop.bank)
        {
            if (holdingZone.holdedItem != null)
            {
                bushes++;
            }
        }
        readyEnabled = bushes >= 8;
        GetComponent<SpriteRenderer>().sprite = readySprites[bushes];

        if (readyEnabled)
        {
            if (hovering)
            {
                GetComponent<SpriteRenderer>().sprite = hoveringSprite;
            }
        }
    }
    
    private void OnMouseExit()
    {
        hovering = false;
    }

    private void OnMouseOver()
    {
        hovering = true;
        if (readyEnabled)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartBattle();
            }
        }
    }

    public void StartBattle()
    {
        foreach (var holdingZone in shop.FightingZones)
        {
            holdingZone.gameObject.SetActive(true);
        }
        shopObject.SetActive(false);
        battleController.GenerateEnemies(4, shop.roundNumber*2);

        if (firstTime) firstTime = false;
        else this.soundManager.PlayBattleMusic();
    }
}
