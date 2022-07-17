using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyButton : MonoBehaviour
{
    public GameObject shop;
    public BattleController battleController;

    private void Start()
    {
        this.battleController = FindObjectOfType<BattleController>();
    }

    private void OnMouseOver()
    {

        if (Input.GetMouseButtonDown(0))
        {
            shop.SetActive(false);
            battleController.GenerateEnemies(4, 1);
        }
    }
}
