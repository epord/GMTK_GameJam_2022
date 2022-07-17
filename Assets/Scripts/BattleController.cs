using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    private DiceFace[] level1Faces;

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
            if (fightingHoles[i].dice != null && enemies.Length >= i)
            {
                Dice playerPlant = fightingHoles[i].dice;
                DiceFace playerFace = playerPlant.selectedFace;
                Enemy enemyPlant = enemies[i];
                DiceFace enemyFace = enemyPlant.enemyFace;

                if (playerFace.attack > enemyFace.defense)
                {
                    enemyPlant.alive = false;
                }

                if (enemyFace.attack > playerFace.defense)
                {
                    playerPlant.alive = false;
                }
            }
        }
    }


    void GenerateEnemies(int numEnemies, int dificulty)
    {
        int[] dificulties = new int[numEnemies];
        int count = 0;
        int indice = 0;
        while (count <= dificulty * numEnemies)
        {
            if (Random.Range(0f, 1f) < 0.5f)
            {
                dificulties[indice] += 1;
                count += 1;
            }
        }
        
    }
}
