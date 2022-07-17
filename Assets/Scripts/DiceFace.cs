using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DiceFace 
{
    public int attack;
    public int defense;
    public Type type;

    public enum Type
    {
        PIMPOLLO,
        OFFENSE_BUG,
        DEFENSE_BUG
    }

    public DiceFace(Type type, int attack, int defense)
    {
        this.type = type;
        this.attack = attack;
        this.defense = defense;
    }
}
