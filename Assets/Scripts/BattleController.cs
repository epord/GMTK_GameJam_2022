using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class BattleController : MonoBehaviour
{
    public Enemy[] enemies;

    public HoldingZone[] fightingHoles;

    public bool doingBattle = false;
    public GameObject singleBushFacePrefab;

    private bool firstTime = true;

    private Shop shop;
    private SoundManager soundManager;
    private SoundEffectManager soundEffectManager;
    
    // Start is called before the first frame update
    void Start()
    {
        shop = FindObjectOfType<Shop>();
        this.soundManager = FindObjectOfType<SoundManager>();
        this.soundEffectManager = FindObjectOfType<SoundEffectManager>();
        FindObjectOfType<ReadyButton>().StartBattle();
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

        yield return new WaitForSeconds(2);
        for (int i = 0; i < 4; i++)
        {

            Dice playerPlant = fightingHoles[i].holdedItem.GetComponent<Dice>();
            int randomNumber = Random.Range(0, 6);

            StartCoroutine(playerPlant.AnimateSelection(randomNumber));
            this.soundEffectManager.PlayRollDice();
            
            yield return new WaitForSeconds(2);
            fightingHoles[i].holdedItem.gameObject.SetActive(false);
            
            playerPlant.selectedFace = playerPlant.diceFaces[randomNumber];
            GameObject newSingleBushFace = Instantiate(singleBushFacePrefab, fightingHoles[i].transform);
            SingleBushFace renderer = newSingleBushFace.GetComponent<SingleBushFace>();
            renderer.SetDiceFace(playerPlant.selectedFace);
            

            SingleBushFace enemyRenderer = enemies[i].transform.GetComponentInChildren<SingleBushFace>();
            renderer.Attack(false);
            enemyRenderer.Attack(true);
            yield return new WaitForSeconds(2);
            
            
                
            DiceFace playerFace = playerPlant.selectedFace;
            Enemy enemyPlant = enemies[i];
            DiceFace enemyFace = enemyPlant.enemyFace;

            if (playerFace.attack > enemyFace.defense)
            {
                Debug.Log("EnemyDead!");
                this.soundEffectManager.PlayPlantDying();
                if(enemyRenderer == null) throw new Exception("BushFaceNotFound!");
                enemyRenderer.Kill();
                kills += 1;
            }
            else
            {
                SpawnEnemy(i);
            }

            if (enemyFace.attack > playerFace.defense)
            {
                Debug.Log("PlayerPlantDead!");
                this.soundEffectManager.PlayPlantDying();
                newSingleBushFace.GetComponent<SingleBushFace>().Kill();
                deaths += 1;
            }else
            {
                Destroy(newSingleBushFace);
                newSingleBushFace = Instantiate(singleBushFacePrefab, fightingHoles[i].transform);
                SingleBushFace newRenderer = newSingleBushFace.GetComponent<SingleBushFace>();
                newRenderer.SetDiceFace(playerPlant.selectedFace);
            }
            
            yield return new WaitForSeconds(2);
        }
        
        yield return new WaitForSeconds(2);

        if (kills < deaths)
        {
            PlayerPrefs.SetInt("score", shop.roundNumber);
            StartCoroutine(this.soundManager.FadeOut(1f));
            StartCoroutine(PlayLoose());
            StartCoroutine(ShowEnd());


            yield return new WaitForSeconds(20);
        }

        foreach (var hole in fightingHoles)
        {
            Grabbable playerGrabbabled = hole.holdedItem;
            SingleBushFace fightingBushResult = hole.GetComponentInChildren<SingleBushFace>();

            if (fightingBushResult.alive)
            {
                playerGrabbabled.gameObject.SetActive(true);
            
                foreach (var holding in shop.bank)
                {
                    if (!holding.IsHoldingItem())
                    {
                        hole.RemoveItem();
                        holding.AssignItem(playerGrabbabled);
                        break;
                    }
                }
            }
            else
            {
                hole.RemoveItem();
                Destroy(playerGrabbabled.gameObject);
            }
            Destroy(fightingBushResult.gameObject);
        }

        
        foreach (var enemy in enemies)
        {
            SingleBushFace enemyBushResult = enemy.GetComponentInChildren<SingleBushFace>();
            Destroy(enemyBushResult.gameObject);
        }
        
        
        shop.RefreshShop();
        shop.gameObject.SetActive(true);
        doingBattle = false;
        this.soundManager.PlayShopMusic();
        this.soundEffectManager.PlayWin();
        FindObjectOfType<Help>().OpenHelp();
    }

    IEnumerator PlayLoose()
    {
        yield return new WaitForSeconds(0f);
        this.soundEffectManager.PlayLoose();

    }

    IEnumerator ShowEnd()
    {
        yield return new WaitForSeconds(6f);
        SceneManager.LoadScene("Score", LoadSceneMode.Single);

    }


    public void GenerateEnemies(int numEnemies, int dificulty)
    {
        Debug.Log(dificulty);
        int[] dificulties = new int[numEnemies];
        int count = 0;
        int indice = 0;
        while (count < dificulty)
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
            SpawnEnemy(i);
        }
    }

    void SpawnEnemy(int i)
    {
        int childCount = enemies[i].transform.childCount;
        if (childCount > 0)
        {
            Destroy(enemies[i].transform.GetChild(0).gameObject);
        }
        GameObject newSingleBushFace = Instantiate(singleBushFacePrefab, enemies[i].transform);
        newSingleBushFace.GetComponent<SingleBushFace>().SetDiceFace(enemies[i].enemyFace);
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
