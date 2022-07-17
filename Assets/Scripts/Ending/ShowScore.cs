using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowScore : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Text>().text = "You survived " + Mathf.CeilToInt(PlayerPrefs.GetInt("score")) + " rounds.";
    }
}
