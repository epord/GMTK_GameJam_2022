using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableDiceFace : Grabbable
{
    public DiceFace diceFace;

    private void Awake()
    {
        this.diceFace = new DiceFace(Random.Range(0, 10), Random.Range(0, 10));
    }
}
