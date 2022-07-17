using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PimpolloComponent : MonoBehaviour
{
    public SpriteRenderer leaf1;
    public SpriteRenderer leaf2;
    public SpriteRenderer leaf3;
    public SpriteRenderer leaf4;
    public SpriteRenderer leaf5;
    public SpriteRenderer pimpollo;
    public Sprite offenseLeaf;
    public Sprite defenseLeaf;
    public Sprite offensePimpollo;
    public Sprite defensePimpollo;
    public Sprite mixedPimpollo;


    public SpriteRenderer getLeaf(int index)
    {
        switch (index)
        {
            case 0: return leaf1;
            case 1: return leaf2;
            case 2: return leaf3;
            case 3: return leaf4;
            case 4: return leaf5;
        }

        return null;
    }

    public void RenderFace(DiceFace diceFace)
    {
        leaf1.sprite = null;
        leaf2.sprite = null;
        leaf3.sprite = null;
        leaf4.sprite = null;
        leaf5.sprite = null;
        pimpollo.sprite = null;

        if (diceFace.attack > 0)
        {
            if (diceFace.defense > 0)
            {
                pimpollo.sprite = mixedPimpollo;
            }
            else
            {
                pimpollo.sprite = offensePimpollo;
            }
        }
        else
        {
            pimpollo.sprite = defensePimpollo;
        }

        if (diceFace.attack > 0)
        {
            leaf1.sprite = offenseLeaf;
        }
        if (diceFace.attack > 1)
        {
            leaf2.sprite = offenseLeaf;
        }
        if (diceFace.attack > 2)
        {
            leaf3.sprite = offenseLeaf;
        }
        if (diceFace.attack > 3)
        {
            leaf4.sprite = offenseLeaf;
        }
        if (diceFace.attack > 4)
        {
            leaf5.sprite = offenseLeaf;
        }

        if (diceFace.defense > 0)
        {
            leaf5.sprite = defenseLeaf;
        }
        if (diceFace.defense > 1)
        {
            leaf4.sprite = defenseLeaf;
        }
        if (diceFace.defense > 2)
        {
            leaf3.sprite = defenseLeaf;
        }
        if (diceFace.defense > 3)
        {
            leaf2.sprite = defenseLeaf;
        }
        if (diceFace.defense > 4)
        {
            leaf1.sprite = defenseLeaf;
        }

    }
}
