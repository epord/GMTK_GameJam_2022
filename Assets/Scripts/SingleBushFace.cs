using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleBushFace : MonoBehaviour
{
    public DiceFace diceFace;
    public GameObject defenseBug;
    public GameObject offenseBug;
    public GameObject pimpolloPrefab;
    public GameObject deadBushPrefab;

    private GameObject pimpollo;

    public bool alive = true;

    public void Kill()
    {
        alive = false;
        Destroy(pimpollo);
        Instantiate(deadBushPrefab, this.transform);
    }

    public void SetDiceFace(DiceFace diceFace)
    {
        this.diceFace = diceFace;
        switch (diceFace.type)
        {
            case DiceFace.Type.PIMPOLLO:
                if (alive)
                {
                    pimpollo = Instantiate(pimpolloPrefab, this.transform);
                    pimpollo.GetComponent<PimpolloComponent>().RenderFace(diceFace);
                }
                break;
            //case DiceFace.Type.DEFENSE_BUG:
            //    Instantiate(defenseBug, this.transform);
            //    break;
            //case DiceFace.Type.OFFENSE_BUG:
            //    Instantiate(offenseBug, this.transform);
            //    break;
        }
    }
}
