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

    private SoundManager soundManager;

    private void Start()
    {
        battleController = FindObjectOfType<BattleController>();
        shop = FindObjectOfType<Shop>();
        shopObject = shop.gameObject;
        this.soundManager = FindObjectOfType<SoundManager>();
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
                foreach (var holdingZone in shop.FightingZones)
                {
                    holdingZone.gameObject.SetActive(true);
                }
                shopObject.SetActive(false);
                battleController.GenerateEnemies(4, shop.roundNumber*2);
                this.soundManager.PlayBattleMusic();
            }
        }
    }
}
