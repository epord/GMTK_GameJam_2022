using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushFace : MonoBehaviour
{
    public DiceFace diceFace;
    public GameObject defenseBug;
    public GameObject offenseBug;
    public GameObject pimpolloPrefab;

    public void SetDiceFace(DiceFace diceFace)
    {
        Debug.Log("SetDiceFace");
        Debug.Log(diceFace.attack);
        Debug.Log(diceFace.defense);
        this.diceFace = diceFace;
        switch (diceFace.type)
        {
            case DiceFace.Type.PIMPOLLO:
                PimpolloComponent pimpollo = GetComponentInChildren<PimpolloComponent>();
                if (pimpollo == null)
                {
                    GameObject newPimpollo = Instantiate(pimpolloPrefab, this.transform);
                    pimpollo = newPimpollo.GetComponent<PimpolloComponent>();
                }
                pimpollo.RenderFace(this.diceFace);
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
