using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BattleController : MonoBehaviour
{
    private Enemy[] enemies;

    private Hole[] fightingHoles;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DoBattle()
    {
        for (int i = 0; i < 4; i++)
        {
            if (fightingHoles[i].dice == null || enemies[i].enemyFace == null)
            {
                throw new Exception();
            }
        }
        
        int kills = 0;
        int deaths = 0;
        
        for (int i = 0; i < 4; i++)
        {
        
            Dice playerPlant = fightingHoles[i].dice;
            DiceFace playerFace = playerPlant.selectedFace;
            Enemy enemyPlant = enemies[i];
            DiceFace enemyFace = enemyPlant.enemyFace;

            if (playerFace.attack > enemyFace.defense)
            {
                enemyPlant.alive = false;
                kills += 1;
            }

            if (enemyFace.attack > playerFace.defense)
            {
                playerPlant.alive = false;
                deaths += 1;
            }
        }

        if (kills >= deaths)
        {
            // You won battle. Go to shop.
            
        }
        else
        {
            // You lost battle. Trigger loss
            
        }
    }


    void GenerateEnemies(int numEnemies, int dificulty)
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
            indice = indice%4;
        }

        for (int i = 0; i < 4; i++)
        {
            DiceFace enemyFace = GenerateFace(dificulties[i]);
            enemies[i].alive = true;
            enemies[i].enemyFace = enemyFace;
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
        DiceFace resultingFace = new DiceFace(attack, defense);
        return resultingFace;
    }
}
