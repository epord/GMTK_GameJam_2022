using System;
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

    private GameObject pimpolloGameObject;

    public bool alive = true;

    public void Kill()
    {
        alive = false;
        Destroy(pimpolloGameObject);
        Instantiate(deadBushPrefab, this.transform);
    }

    public void SetDiceFace(DiceFace diceFace)
    {
        this.diceFace = diceFace;
        switch (diceFace.type)
        {
            case DiceFace.Type.PIMPOLLO:
                PimpolloComponent pimpollo = GetComponentInChildren<PimpolloComponent>();
                if (pimpollo == null)
                {
                    pimpolloGameObject = Instantiate(pimpolloPrefab, this.transform);
                    pimpollo = pimpolloGameObject.GetComponent<PimpolloComponent>();
                }
                pimpollo.RenderFace(this.diceFace);
                break;
            //case DiceFace.Type.DEFENSE_BUG:
            //    Instantiate(defenseBug, this.transform);
            //    break;
            //case DiceFace.Type.OFFENSE_BUG:
            //    Instantiate(offenseBug, this.transform);
            //    break;
        }
    }

    public IEnumerator MoveAttackingPlants(bool isEnemy)
    {
        PimpolloComponent pimpollo = GetComponentInChildren<PimpolloComponent>();

        float time = 0f;
        while (time < 3 && gameObject.activeInHierarchy)
        {
            time += Time.deltaTime;
            for (int i = 0; i < diceFace.attack; i++)
            {
                try
                {
                    GameObject leaf = pimpollo.getLeaf(i).gameObject;

                    if (isEnemy)
                    {
                        leaf.transform.Translate(-10 * Time.deltaTime, 0, 0);
                    }
                    else
                    {
                        leaf.transform.Translate(8 * Time.deltaTime, 0, 0);
                    }
                }
                catch (Exception e)
                {
                }

                yield return new WaitForEndOfFrame();
            }
        }

    }
    
    
    public IEnumerator GrowDefense()
    {
        PimpolloComponent pimpollo = GetComponentInChildren<PimpolloComponent>();

        float time = 0f;
        while (time < 0.5f && gameObject.activeInHierarchy)
        {
            time += Time.deltaTime;
            for (int i = 4; i >4-diceFace.defense; i--)
            {
                try
                {
                    GameObject leaf = pimpollo.getLeaf(i).gameObject;
                    if (leaf == null)
                    {
                        continue;
                    }

                    leaf.transform.localScale += new Vector3(0.5f * Time.deltaTime, 0.5f * Time.deltaTime,
                        0.5f * Time.deltaTime);
                    
                    // collect the direction
                    Vector3 dir = (transform.position - leaf.transform.position).normalized;
                    // translate at speed
                    leaf.transform.Translate(dir * 12f * Time.deltaTime);
                }
                catch (Exception e)
                {
                }

                yield return new WaitForEndOfFrame();
            }
        }

    }
    
    public void Attack(bool isEnemy)
    {
        StartCoroutine(MoveAttackingPlants(isEnemy));
        StartCoroutine(GrowDefense());

    }
}
