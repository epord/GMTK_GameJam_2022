using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushFace : MonoBehaviour
{
    public DiceFace diceFace;
    public GameObject defenseBug;
    public GameObject offenseBug;
    public GameObject pimpollo;

    public void SetDiceFace(DiceFace diceFace)
    {
        this.diceFace = diceFace;
        switch (diceFace.type)
        {
            case DiceFace.Type.PIMPOLLO:
                Instantiate(pimpollo, this.transform);
                pimpollo.GetComponent<PimpolloComponent>().RenderFace(diceFace);
                break;
            case DiceFace.Type.DEFENSE_BUG:
                Instantiate(defenseBug, this.transform);
                break;
            case DiceFace.Type.OFFENSE_BUG:
                Instantiate(offenseBug, this.transform);
                break;
        }
    }
}
