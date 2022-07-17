using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Help : MonoBehaviour
{
    
    private bool hovering = false;

    public GameObject helpScreen;
    public GameObject helpScreen2;

    public int active = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (active == 0)
            {
                active = 1;
            }
            else if (active == 1)
            {
                helpScreen.SetActive(false);
                helpScreen2.SetActive(true);
                active = 2;
            }
            else
            {
                helpScreen.SetActive(false);
                helpScreen2.SetActive(false);
                active = 3;
            }
        }
    }
    
    private void OnMouseExit()
    {
        hovering = false;
    }

    private void OnMouseOver()
    {
        hovering = true;
        if (Input.GetMouseButtonUp(0))
        {
            OpenHelp();
        }
    }

    public void OpenHelp()
    {
        helpScreen.SetActive(true);
        active = 0;
    }
    
}
