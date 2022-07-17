using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BattleController : MonoBehaviour
{
    public Enemy[] enemies;

    public HoldingZone[] fightingHoles;

    public bool doingBattle = false;
    public GameObject singleBushFacePrefab;

    private Shop shop;
    
    // Start is called before the first frame update
    void Start()
    {
        shop = FindObjectOfType<Shop>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach(HoldingZone holdingZone in fightingHoles)
        {
            if (!holdingZone.IsHoldingItem())
            {
                return;
            }
        }
        if (!this.doingBattle)
        {
            StartCoroutine(this.DoBattle());
        }
    }

    IEnumerator DoBattle()
    {
        doingBattle = true;
        
        for (int i = 0; i < 4; i++)
        {
            if (fightingHoles[i].holdedItem.GetComponent<Dice>() == null || enemies[i].enemyFace == null)
            {
                throw new Exception();
            }
        }
        
        int kills = 0;
        int deaths = 0;

        for (int i = 0; i < 4; i++)
        {
            Dice playerPlant = fightingHoles[i].holdedItem.GetComponent<Dice>();
            fightingHoles[i].holdedItem.gameObject.SetActive(false);

            int randomNumber = Random.Range(0, 6);
            playerPlant.selectedFace = playerPlant.diceFaces[randomNumber];
            GameObject newSingleBushFace = Instantiate(singleBushFacePrefab, fightingHoles[i].transform);
            newSingleBushFace.GetComponent<SingleBushFace>().SetDiceFace(playerPlant.selectedFace);

            yield return new WaitForSeconds(2);
                
            DiceFace playerFace = playerPlant.selectedFace;
            Enemy enemyPlant = enemies[i];
            DiceFace enemyFace = enemyPlant.enemyFace;

            if (playerFace.attack > enemyFace.defense)
            {
                Debug.Log("EnemyDead!");
                SingleBushFace bushFace = enemyPlant.gameObject.GetComponentInChildren<SingleBushFace>();
                if(bushFace == null) throw new Exception("BushFaceNotFound!");
                bushFace.Kill();
                kills += 1;
            }

            if (enemyFace.attack > playerFace.defense)
            {
                Debug.Log("PlayerPlantDead!");    
                newSingleBushFace.GetComponent<SingleBushFace>().Kill();
                deaths += 1;
            }
            
            yield return new WaitForSeconds(2);
        }
        
        yield return new WaitForSeconds(2);

        if (kills < deaths)
        {
            // You lost battle. Trigger loss
         
        }

        foreach (var hole in fightingHoles)
        {
            Grabbable gameObject = hole.holdedItem;
            foreach (var holding in shop.bank)
            {
                if (holding.holdedItem == null)
                {
                    holding.AssignItem(gameObject);
                    gameObject.enabled = true;
                    hole.RemoveItem();
                }
            }
        }

        
        /*foreach (var enemy in enemies)
        {
            enemy.enemyFace = null;
        }*/
        
        shop.gameObject.SetActive(true);
        doingBattle = false;
    }


    public void GenerateEnemies(int numEnemies, int dificulty)
    {
        int[] dificulties = new int[numEnemies];
        int count = 0;
        int indice = 0;
        while (count <= dificulty)
        {
            if (Random.Range(0f, 1f) < 0.5f)
            {
                if (dificulties[indice] < 4)
                {
                    dificulties[indice] += 1;
                }
                count += 1;
            }
            indice += 1;
            indice = indice % 4;
        }

        for (int i = 0; i < 4; i++)
        {
            DiceFace enemyFace = GenerateFace(dificulties[i]);
            enemies[i].enemyFace = enemyFace;
            GameObject newSingleBushFace = Instantiate(singleBushFacePrefab, enemies[i].transform);
            newSingleBushFace.GetComponent<SingleBushFace>().SetDiceFace(enemyFace);
        }
    }

    DiceFace GenerateFace(int dificulty)
    {
        int attack = 0;
        int defense = 0;
        for (int i = 0; i <= dificulty; i++)
        {
            if (Random.Range(0f, 1f) < 0.5f)
            {
                attack += 1;
            }
            else
            {
                defense += 1;
            }
        }
        DiceFace resultingFace = new DiceFace(DiceFace.Type.PIMPOLLO ,attack, defense);
        return resultingFace;
    }
}
