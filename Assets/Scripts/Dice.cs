using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Dice : MonoBehaviour
{
    [SerializeField]
    public DiceFace[] diceFaces;

    public DiceFace selectedFace;
    public Transform[] flowerLocations;
    public GameObject bushFacePrefab;

    public bool alive;

    void Awake()
    {
        this.diceFaces = new DiceFace[6];
        for (int i = 0; i < 6; i++)
        {
            float random = Random.RandomRange(0f, 1f);
            if (random < 0.3f)
            {
                this.diceFaces[i] = new DiceFace(DiceFace.Type.PIMPOLLO, 1, 0);
            }
            else if(random < 0.5)
            {
                this.diceFaces[i] = new DiceFace(DiceFace.Type.PIMPOLLO, 2, 0);
            } else if (random < 0.7) {
                this.diceFaces[i] = new DiceFace(DiceFace.Type.PIMPOLLO, 0, 1);
            }
            else
            {
                this.diceFaces[i] = new DiceFace(DiceFace.Type.PIMPOLLO, 1, 1);
            }
            
            GameObject newBushFace = Instantiate(bushFacePrefab, this.flowerLocations[i].transform);
            newBushFace.GetComponent<BushFace>().SetDiceFace(this.diceFaces[i]);
        }

    }

    public void ReplaceFace(int idx, DiceFace diceFace)
    {
        this.diceFaces[idx] = diceFace;
        flowerLocations[idx].GetComponentInChildren<BushFace>().SetDiceFace(diceFace);
    }

    public void SetDiceFaces(DiceFace[] newDiceFaces)
    {
        for(int i = 0; i < 6; i++)
        {
            this.ReplaceFace(i, newDiceFaces[i]);
        }
    }

    public IEnumerator AnimateFlower(int index)
    {
        ParticleSystem system = flowerLocations[index].GetComponentInChildren<ParticleSystem>();
        system.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        system.Play();
        yield return 0;
    }
    
    public IEnumerator PLayDaddy(int index)
    {
        ParticleSystem system = flowerLocations[index].GetComponentsInChildren<ParticleSystem>()[2];
        system.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        system.Play();
        yield return 0;
    }
    
    public IEnumerator PLayMommy(int index)
    {
        ParticleSystem system = flowerLocations[index].GetComponentsInChildren<ParticleSystem>()[3];
        system.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        system.Play();
        yield return 0;
    }

    public IEnumerator AnimateSelection(int selectedIndex)
    {
        for(int i = 0; i < 6; i++)
        {
            StartCoroutine(AnimateFlower(i));
            yield return new WaitForSeconds(0.2f);
        }
        ParticleSystem selectdSystem = flowerLocations[selectedIndex].GetComponentsInChildren<ParticleSystem>()[1];
        selectdSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        selectdSystem.Play();
    }


}
