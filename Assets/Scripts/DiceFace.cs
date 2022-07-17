using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DiceFace 
{
    public int attack;
    public int defense;
    public bool firstAttack;
    public bool buff;
    public bool debuff;

    public DiceFace(int attack, int defense)
    {
        this.attack = attack;
        this.defense = defense;
    }
}
